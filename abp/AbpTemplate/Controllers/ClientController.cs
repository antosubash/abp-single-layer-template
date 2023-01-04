using AbpTemplate.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.OpenIddict.Applications;
using System.Text.Json;
namespace AbpTemplate.Controllers;

[Route("api/client-management")]
public class ClientController : AbpController
{
    private readonly IRepository<OpenIddictApplication, Guid> _openIddictApplicationRepository;
    public ClientController(IRepository<OpenIddictApplication, Guid> _openIddictApplicationRepository)
    {
        this._openIddictApplicationRepository = _openIddictApplicationRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClientDto>>> GetListAsync()
    {
        var clients = await _openIddictApplicationRepository.GetListAsync();
        return Ok(clients.Select(x => new ClientDto {
            ClientId = x.ClientId,
            DisplayName = x.DisplayName,
            PostLogoutRedirectUris = x.PostLogoutRedirectUris,
            RedirectUris = x.RedirectUris,
            Permissions = x.Permissions,
            Type = x.Type
        }).ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDto>> GetAsync(Guid id)
    {
        var client = await _openIddictApplicationRepository.GetAsync(id);
        return Ok(new ClientDto {
            ClientId = client.ClientId,
            DisplayName = client.DisplayName,
            PostLogoutRedirectUris = client.PostLogoutRedirectUris,
            RedirectUris = client.RedirectUris,
            Permissions = client.Permissions,
            Type = client.Type
        });
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> CreateAsync(ClientDto input)
    {
        var client = new OpenIddictApplication(GuidGenerator.Create());
        client.ClientId = input.ClientId;
        client.DisplayName = input.DisplayName;
        client.PostLogoutRedirectUris = input.PostLogoutRedirectUris;
        client.RedirectUris = input.RedirectUris;
        client.Permissions = input.Permissions;
        client.Type = input.Type;
        await _openIddictApplicationRepository.InsertAsync(client);
        return Ok(new ClientDto {
            ClientId = client.ClientId,
            DisplayName = client.DisplayName,
            PostLogoutRedirectUris = client.PostLogoutRedirectUris,
            RedirectUris = client.RedirectUris,
            Permissions = client.Permissions,
            Type = client.Type
        });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ClientDto>> UpdateAsync(Guid id, ClientDto input)
    {
        var client = await _openIddictApplicationRepository.GetAsync(id);
        client.ClientId = input.ClientId;
        client.DisplayName = input.DisplayName;
        client.PostLogoutRedirectUris = input.PostLogoutRedirectUris;
        client.RedirectUris = input.RedirectUris;
        client.Permissions = input.Permissions;
        client.Type = input.Type;
        await _openIddictApplicationRepository.UpdateAsync(client);
        return Ok(new ClientDto {
            ClientId = client.ClientId,
            DisplayName = client.DisplayName,
            PostLogoutRedirectUris = client.PostLogoutRedirectUris,
            RedirectUris = client.RedirectUris,
            Permissions = client.Permissions,
            Type = client.Type
        });
    }

    [HttpPost("add-redirect-uri/{id}")]
    public async Task<ActionResult<ClientDto>> AddRedirectUriAsync(Guid id, string redirectUri)
    {
        var client = await _openIddictApplicationRepository.GetAsync(id);
        var redirectUris = JsonSerializer.Deserialize<List<string>>(client.RedirectUris);
        redirectUris.Add(redirectUri);
        client.RedirectUris = JsonSerializer.Serialize(redirectUris);
        await _openIddictApplicationRepository.UpdateAsync(client);
        return Ok(new ClientDto {
            ClientId = client.ClientId,
            DisplayName = client.DisplayName,
            PostLogoutRedirectUris = client.PostLogoutRedirectUris,
            RedirectUris = client.RedirectUris,
            Permissions = client.Permissions,
            Type = client.Type
        });
    }

    [HttpPost("add-post-logout-redirect-uri/{id}")]
    public async Task<ActionResult<ClientDto>> AddPostLogoutRedirectUriAsync(Guid id, string redirectUri)
    {
        var client = await _openIddictApplicationRepository.GetAsync(id);
        var redirectUris = JsonSerializer.Deserialize<List<string>>(client.PostLogoutRedirectUris);
        redirectUris.Add(redirectUri);
        client.PostLogoutRedirectUris = JsonSerializer.Serialize(redirectUris);
        await _openIddictApplicationRepository.UpdateAsync(client);
        return Ok(new ClientDto {
            ClientId = client.ClientId,
            DisplayName = client.DisplayName,
            PostLogoutRedirectUris = client.PostLogoutRedirectUris,
            RedirectUris = client.RedirectUris,
            Permissions = client.Permissions,
            Type = client.Type
        });
    }

    [HttpPost("update-client-type/{id}")]
    public async Task<ActionResult<ClientDto>> UpdateClientTypeAsync(Guid id, string clientType)
    {
        var client = await _openIddictApplicationRepository.GetAsync(id);
        client.Type = clientType;
        await _openIddictApplicationRepository.UpdateAsync(client);
        return Ok(new ClientDto {
            ClientId = client.ClientId,
            DisplayName = client.DisplayName,
            PostLogoutRedirectUris = client.PostLogoutRedirectUris,
            RedirectUris = client.RedirectUris,
            Permissions = client.Permissions,
            Type = client.Type
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var client = await _openIddictApplicationRepository.GetAsync(id);
        await _openIddictApplicationRepository.DeleteAsync(client);
        return Ok();
    }

}