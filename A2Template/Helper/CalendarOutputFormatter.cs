using A2.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace A2.Helper
{
    public class CalenderOutputFormatter : TextOutputFormatter
    {
        public CalenderOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            SupportedEncodings.Add(Encoding.UTF8);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            Event userEvent = (Event)context.Object;
            string userTime = DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ");
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("BEGIN:VCALEBDAR");
            builder.AppendLine("VERSION:4.0");
            builder.AppendLine("PRODID:" + "skri486");
            builder.AppendLine("BEGIN:VEVENT");
            builder.AppendLine("UID:" + userEvent.Id);
            builder.AppendLine("DTSTAMP:" + userTime);
            builder.AppendLine("DTSTART:" + userEvent.Start);
            builder.AppendLine("DTEND:" + userEvent.End);
            builder.AppendLine("SUMMARY:" + userEvent.Summary);
            builder.AppendLine("DESCRIPTION:" + userEvent.Description);
            builder.AppendLine("LOCATION:" + userEvent.Location);
            builder.AppendLine("END:VEVENT");
            builder.AppendLine("END:VCALENDAR");
            string outString = builder.ToString();
            byte[] outBytes = selectedEncoding.GetBytes(outString);
            var response = context.HttpContext.Response.Body;
            return response.WriteAsync(outBytes, 0, outBytes.Length);
        }
    }



    
}

