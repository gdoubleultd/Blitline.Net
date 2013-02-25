﻿using System;
using System.Linq;
using Blitline.Net.Builders;
using Blitline.Net.Request;
using Blitline.Net.Response;
using SubSpec;
using Xunit;

namespace Specs.Integration
{
    public class ProcessMultipageDocument
    {
        [Specification]
        public void CanProcessAMultipageDocument()
        {
            var request = default(BlitlineRequest);
            var response = default(BlitlineResponse);

            "Given I have a screenshot request".Context(() =>
            {
                const string bucketName = "gdoubleu-test-photos";

                request = BuildA.Request(r => r
                                .WithApplicationId("a5KqkemeX2RttyYdkOrdug")
                                .WithSourceImageUri(new Uri("https://s3-eu-west-1.amazonaws.com/gdoubleu-test-photos/Gareth+Williams%27s+Resume.pdf"))
                                .SourceIsMultipageDocument()
                                .ResizeToFit(f => f.WithWidth(200).WithHeight(200)
                                    .SaveAs(s => s.WithImageIdentifier("multipage_1")
                                    .ToS3(s3 => s3.ToBucket(bucketName).WithKey("multipage.png")))));
            });

            "When I process the request".Do(() => response = request.Send());

            "Then the s3 url should not be empty".Observation(() => Assert.NotEmpty(response.Results.Images.First().S3Url));
        }

        [Specification]
        public void CanProcessAMultipageDocumentWithSpecificPages()
        {
            var request = default(BlitlineRequest);
            var response = default(BlitlineResponse);

            "Given I have a screenshot request".Context(() =>
            {
                const string bucketName = "gdoubleu-test-photos";

                request = BuildA.Request(r => r
                                .WithApplicationId("a5KqkemeX2RttyYdkOrdug")
                                .WithSourceImageUri(new Uri("https://s3-eu-west-1.amazonaws.com/gdoubleu-test-photos/Gareth+Williams%27s+Resume.pdf"))
                                .SourceIsMultipageDocument(new[] { 1 })
                                .ResizeToFit(f => f.WithWidth(200).WithHeight(200)
                                    .SaveAs(s => s.WithImageIdentifier("multipage_2")
                                    .ToS3(s3 => s3.ToBucket(bucketName).WithKey("multipage_page.png")))));
            });

            "When I process the request".Do(() => response = request.Send());

            "Then the s3 url should not be empty".Observation(() => Assert.NotEmpty(response.Results.Images.First().S3Url));
        }
    }
}