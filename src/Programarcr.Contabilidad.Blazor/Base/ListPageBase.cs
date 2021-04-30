using Blazorise;
using Blazorise.DataGrid;
using JetBrains.Annotations;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Components;

namespace Programarcr.Contabilidad.Blazor.Base
{
    public abstract class ListPageBase<TAppService,
            TGetOutputDto,
            TGetListOutputDto,
            TKey,
            TGetListInput,
            TListViewModel>
        : ContabilidadComponentBase
        where TAppService : IReadOnlyAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>
        where TGetOutputDto : IEntityDto<TKey>
        where TGetListOutputDto : IEntityDto<TKey>
        where TGetListInput : new()
        where TListViewModel : IEntityDto<TKey>

    {
        [Inject] protected TAppService AppService { get; set; }
        [Inject] protected IStringLocalizer<AbpUiResource> UiLocalizer { get; set; }

        protected virtual int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;

        protected int CurrentPage = 1;
        protected string CurrentSorting;
        protected int? TotalCount;
        protected TGetListInput GetListInput = new TGetListInput();
        protected IReadOnlyList<TListViewModel> Entities = Array.Empty<TListViewModel>();

        protected override async Task OnInitializedAsync()
        {
            await SetBreadcrumbItemsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            return ValueTask.CompletedTask;
        }

        protected virtual async Task GetEntitiesAsync()
        {
            await UpdateGetListInputAsync();
            var result = await AppService.GetListAsync(GetListInput);
            Entities = MapToListViewModel(result.Items);
            TotalCount = (int?)result.TotalCount;
        }

        private IReadOnlyList<TListViewModel> MapToListViewModel(IReadOnlyList<TGetListOutputDto> dtos)
        {
            if (typeof(TGetListOutputDto) == typeof(TListViewModel))
            {
                return dtos.As<IReadOnlyList<TListViewModel>>();
            }

            return ObjectMapper.Map<IReadOnlyList<TGetListOutputDto>, List<TListViewModel>>(dtos);
        }
        protected virtual Task UpdateGetListInputAsync()
        {
            if (GetListInput is ISortedResultRequest sortedResultRequestInput)
            {
                sortedResultRequestInput.Sorting = CurrentSorting;
            }

            if (GetListInput is IPagedResultRequest pagedResultRequestInput)
            {
                pagedResultRequestInput.SkipCount = (CurrentPage - 1) * PageSize;
            }

            if (GetListInput is ILimitedResultRequest limitedResultRequestInput)
            {
                limitedResultRequestInput.MaxResultCount = PageSize;
            }

            return Task.CompletedTask;
        }

        protected virtual async Task SearchEntitiesAsync()
        {
            CurrentPage = 1;

            await GetEntitiesAsync();

            StateHasChanged();
        }

        protected virtual async Task OnDataGridReadAsync(DataGridReadDataEventArgs<TListViewModel> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.Direction != SortDirection.None)
                .Select(c => c.Field + (c.Direction == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;

            await GetEntitiesAsync();

            StateHasChanged();
        }

        protected virtual async Task CheckPolicyAsync([CanBeNull] string policyName)
        {
            if (string.IsNullOrEmpty(policyName))
            {
                return;
            }

            await AuthorizationService.CheckAsync(policyName);
        }


    }
}
