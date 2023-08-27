using WebApi.Utilities.Formatters;

namespace WebApi.Extensions
{
    public static class IMvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder builder)
        {
            return builder.AddMvcOptions(options =>
            {
                options.OutputFormatters
                .Add(new CsvOutputFormatter());
            });
        }
    }
}
