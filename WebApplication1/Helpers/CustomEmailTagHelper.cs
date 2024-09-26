﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication1.Helpers
{
    public class CustomEmailTagHelper:TagHelper
    {
        public string MyEmail { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:fatima.d@cgg.gov.in");
            //output.Attributes.SetAttribute("href", $"mailto:{MyEmail}");
            output.Attributes.Add("id", "my-email-id");
            output.Content.SetContent("my-email");
            //base.Process(context, output);
        }
    }
}
