using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using Programarcr.Contabilidad.Cuentas;
using Volo.Abp.AspNetCore.Components;
using System.Collections;
using Microsoft.Extensions.Logging;
using Programarcr.Contabilidad.Configuracion;

namespace Programarcr.Contabilidad.Blazor.Pages
{
    public partial class PruebaImportacionCuentasModel : ContabilidadComponentBase
    {

        [Inject]
        protected ICuentasAppService cuentasAppService { get; set; }

        [Inject]
        protected ICuentaFormatProvider cuentaFormatProvider { get; set; }

        [Inject]
        protected IMonedaAppService monedasAppService { get; set; }

        protected FileEdit fileEdit;
        protected List<CreateCuentaDto> Cuentas = new List<CreateCuentaDto>();

        protected bool DisabledButtonImport = true;

        protected bool CuentasInportadasEvent = false;
        protected bool ShowResultReadExcel = true;

        
        
        protected async Task OnChanged(FileChangedEventArgs e)
        {
            try
            {
                foreach(var file in e.Files)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.WriteToStreamAsync(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        Cuentas = await ReadExcel(stream);
                        if (Cuentas.Any())
                        {
                            DisabledButtonImport = false;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                await Notify.Error("Error al cargar el archivo.");
                Logger.LogError(exc.Message);
            }
            finally
            {                                   
                StateHasChanged();
            }
        }
        
        protected async Task ButtonImportPressed()
        {
            DisabledButtonImport = !DisabledButtonImport;
            await GuardarCuentasDetectadas();
            CuentasInportadasEvent = true;
            ShowResultReadExcel = false;
            StateHasChanged();
        }

        protected async Task GuardarCuentasDetectadas()
        {
            for (int i = 0; i < 7; i++)
            {
                var cuentasDeNivelN = Cuentas
                    .Where(importExcelRow => importExcelRow.Level == i && importExcelRow.IsValid)
                    .ToList();

                var resultadoImportCuentas = await cuentasAppService.CrearCuentas(cuentasDeNivelN);
                
                foreach (var item in resultadoImportCuentas)
                {
                    var cuenta = Cuentas.First(x => x.NumeroCuenta == item.NumeroCuenta);

                    cuenta.Resultado = item.ErrorMessageResult;
                    cuenta.IsValid = item.IsCreated;
                }
            }
        }
        

        #region Excel reader helpers
        private async Task<CreateCuentaDto> RowToObjectCuentaDto(ExcelWorksheet worksheet, int rowNumber)
        {
            var monedas = await monedasAppService.GetListAsync(new MonedaFilterDto { IsEnabled = true, MaxResultCount = 1000 });
            
            string numero = worksheet.Cells[rowNumber, CuentaColumnasExcelNumber.NUMERO_CUENTA_COL_N].GetValue<string>();
            int level = cuentaFormatProvider.GetLevelOf(numero);
            var fila = new CreateCuentaDto
            {
                Nombre = worksheet.Cells[rowNumber, CuentaColumnasExcelNumber.NOMBRE_COL_N].GetValue<string>(),
                TipoCuenta = CuentaColumnasExcelNumber.StringToCuentaTypeEnum(worksheet.Cells[rowNumber, CuentaColumnasExcelNumber.TIPO_CUENTA_COL_N].GetValue<string>()),
                Moneda = worksheet.Cells[rowNumber, CuentaColumnasExcelNumber.MONEDA_COL_N].GetValue<string>(),
                TipoBalance = CuentaColumnasExcelNumber.StringToBalanceTypeEnum(worksheet.Cells[rowNumber, CuentaColumnasExcelNumber.TIPO_BALANCE_COL_N].GetValue<string>()),
                NumeroCuenta = numero,
                Level = level,
                ParentId = level > 0 ? cuentaFormatProvider.GetParent(numero) : null,
                IsValid = true
            };

            if (!monedas.Items.Any(x => x.Id == fila.Moneda))
            {
                fila.IsValid = false;
                fila.Resultado = $"La moneda {fila.Moneda} no es válida o no está habilitada";
            }
                
            return fila;
        }
        
        
        protected async Task<List<CreateCuentaDto>> ReadExcel(Stream fileStream)
        {
            var listCuentas = new List<CreateCuentaDto>();
            fileStream.Seek(0L, SeekOrigin.Begin);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            
            using(var package = new ExcelPackage(fileStream))
            {
                var firstWorkSheet = package.Workbook.Worksheets.First();
                var currentRow = 2;
                while (HasValueInCuentaCol(firstWorkSheet, currentRow))
                {
                    try
                    {
                        var item = await RowToObjectCuentaDto(firstWorkSheet, currentRow);
                        listCuentas.Add(item);
                    }
                    catch (Exception e)
                    {
                        Logger.LogWarning(e.Message);
                        listCuentas.Add(new CreateCuentaDto()
                        {
                            Level = -1,
                            Resultado = $"Error al leer la fila {currentRow}",
                            NumeroCuenta = $"Fila {currentRow}",
                            IsValid = false
                        });
                    }
                    currentRow++;
                }
            }
            return listCuentas;
        }
        #endregion
        
        
        
        #region Helpers
        
        private static bool HasValueInCuentaCol(ExcelWorksheet worksheet, int rowNumber)
        {
            return !string.IsNullOrEmpty(worksheet.Cells[rowNumber, CuentaColumnasExcelNumber.NUMERO_CUENTA_COL_N]
                .GetValue<string>());
        }

        public static class CuentaColumnasExcelNumber
        {
            public const int NUMERO_CUENTA_COL_N = 1;
            public const int TIPO_CUENTA_COL_N = 2;
            public const int MONEDA_COL_N = 3;
            public const int TIPO_BALANCE_COL_N = 4;
            public const int NOMBRE_COL_N = 5;

            public static readonly Dictionary<int, string> VALUES = new Dictionary<int, string>()
            {
                { NUMERO_CUENTA_COL_N, "Numero de cuenta*" },
                { TIPO_CUENTA_COL_N, "Tipo de cuenta*" },
                { MONEDA_COL_N, "Moneda*" },
                { TIPO_BALANCE_COL_N, "Tipo de balance*" },
                { NOMBRE_COL_N, "Nombre*" }
            };


            #region Normalizers
            public static string NormalizeMoneda(string moneda)
            {
                return moneda.Trim().ToUpper();
            }

            public static string NormalizeNumeroCuenta(string numeroCuenta)
            {
                return numeroCuenta.Trim();
            }

            public static string NormalizeNombre(string nombre)
            {
                return nombre.Trim();
            }

            #endregion



            #region Enum Converters

            public static BalanceTypeEnum StringToBalanceTypeEnum(string input)
            {
                const string inicialDebido = "D";
                const string inicialCredito = "C";
                if (input.ToUpper() == inicialCredito)
                {
                    return BalanceTypeEnum.Credito;
                }
                else if (input.ToUpper() == inicialDebido)
                {
                    return BalanceTypeEnum.Credito;
                }
                return (BalanceTypeEnum)Enum.Parse(typeof(BalanceTypeEnum), input, ignoreCase: true);
            }

            public static string BalanceTypeEnumToString(BalanceTypeEnum input)
            {
                return input.ToString("g");
            }

            public static CuentaTypeEnum StringToCuentaTypeEnum(string input)
            {
                return (CuentaTypeEnum)Enum.Parse(typeof(CuentaTypeEnum), input, ignoreCase: true);
            }

            public static string CuentaTypeEnumToString(CuentaTypeEnum input)
            {
                return input.ToString("g");
            }

            #endregion
        }
        
        #endregion
        

    }
}