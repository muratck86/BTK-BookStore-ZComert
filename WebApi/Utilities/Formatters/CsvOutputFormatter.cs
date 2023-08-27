using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace WebApi.Utilities.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if(typeof(BookDto).IsAssignableFrom(type) 
                || typeof(IEnumerable<BookDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        private static void FormatCsv(StringBuilder buffer, BookDto book)
        {
            buffer.AppendLine($"{book.Id},{book.Title},{book.Price}");
        }

        public override async Task WriteResponseBodyAsync(
            OutputFormatterWriteContext context,
            Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            buffer.AppendLine("Id,Title,Price");

            if(context.Object is BookDto bookDto)
            {
                FormatCsv(buffer, (BookDto)context.Object);
            }
            else
            {
                foreach(var book in (IEnumerable<BookDto>)context.Object)
                {
                    FormatCsv(buffer, book);
                }
            }
            await response.WriteAsync(buffer.ToString());
        }
    }
}
