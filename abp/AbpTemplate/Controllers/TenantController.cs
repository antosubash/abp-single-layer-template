using AbpTemplate.Permissions;
using AbpTemplate.Repository;
using AbpTemplate.Services.Dtos;
using AbpTemplate.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Data;

namespace AbpTemplate.Controllers;

[Route("api/multi-tenancy")]
[Authorize(AbpTemplatePermissions.Tenant.Default)]
public class TenantController : AbpController
{
    private readonly CustomTenantRepository _tenantRepository;

    public TenantController(CustomTenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<Guid>> GetTenantGuid(string host)
    {
        var tenant = await _tenantRepository.GetTenantByHost(host);
        if(tenant == null)
        {
            return Ok();
        }
        return Ok(tenant.Id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomTenantDto>> GetTenantHost(Guid id)
    {
        var tenant = await _tenantRepository.GetAsync(id);
        var host = tenant.GetProperty<string>(Constant.Host);
        return new CustomTenantDto
        {
            Id = tenant.Id,
            Name = tenant.Name,
            Host = host
        };
    }

    [HttpPost]
    [Authorize(AbpTemplatePermissions.Tenant.AddHost)]
    public async Task<ActionResult<CustomTenantDto>> AddHost(AddHostDto dto)
    {
        var tenant = await _tenantRepository.GetAsync(dto.Id);
        tenant.SetProperty(Constant.Host, dto.Host);
        await _tenantRepository.UpdateAsync(tenant);
        return new CustomTenantDto
        {
            Id = tenant.Id,
            Name = tenant.Name,
            Host = dto.Host
        };
    }
}