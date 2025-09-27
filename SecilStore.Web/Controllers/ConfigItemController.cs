using Microsoft.AspNetCore.Mvc;
using SecilStore.Common.DTOs;
using SecilStore.Common.Results;

public class ConfigItemController : Controller
{
    private readonly HttpClient _httpClient;

    public ConfigItemController(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("ConfigApi");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetFromJsonAsync<DataResult<ConfigurationItemData>>("/secil-store/config-item/list");
        return View(response.Data?.data);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ConfigurationItemDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("/secil-store/config-item", dto);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ConfigurationItemDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync("/secil-store/config-item/passives", dto);
        return RedirectToAction("Index");
    }
}
