using EShopSample;
using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("EShopSample.Web.csproj");
await builder.RunAbpModuleAsync<EShopSampleWebTestModule>(applicationName: "EShopSample.Web" );

public partial class Program
{
}