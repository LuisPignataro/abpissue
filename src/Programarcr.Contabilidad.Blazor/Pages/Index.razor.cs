using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Programarcr.Contabilidad.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Programarcr.Contabilidad.Blazor.Pages
{
    public partial class Index
    {
        IOptions<AbpLocalizationOptions> abpLocalizationOptions { get; set; }
        ResourceManagerStringLocalizerFactory InnerFactory { get; set; }
        ILanguageProvider LanguageProvider;
        IVirtualFileProvider VirtualFileProvider;

        public Index()
        {
        }

        public Index(IOptions<AbpLocalizationOptions> abpLocalizationOptions, 
            ResourceManagerStringLocalizerFactory innerFactory, 
            ILanguageProvider languageProvider, IVirtualFileProvider virtualFileProvider) : this()
        {
            this.abpLocalizationOptions = abpLocalizationOptions;
            InnerFactory = innerFactory;
            LanguageProvider = languageProvider;
            VirtualFileProvider = virtualFileProvider;
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Console.WriteLine("-----------------------------------------------");
                var lang = await LanguageProvider.GetLanguagesAsync();

                Console.WriteLine(CultureInfo.CurrentCulture.Name);

                var AbpLocalizationOptions = abpLocalizationOptions.Value;

                Type resourceType = typeof(ContabilidadResource);
                var resource = AbpLocalizationOptions.Resources.GetOrDefault(resourceType);

                Console.WriteLine((resource == null).ToString(), resource);

                var Localizer = InnerFactory.Create(resourceType);

                Console.WriteLine((Localizer == null).ToString(), Localizer);

                Console.WriteLine(Localizer.GetString("LongWelcomeMessage"));

                var all = Localizer.GetAllStrings();
                Console.WriteLine($"{all.Count()} {all.First().Name}", all);

                Console.WriteLine($"Contributors : {resource.Contributors.Count}");

                Console.WriteLine("-----------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
        }
    }
}
