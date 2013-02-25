﻿using System.Linq;
using Blitline.Net;
using Blitline.Net.Functions;
using Blitline.Net.Request;
using Blitline.Net.Response;
using SubSpec;
using Xunit;

namespace Specs.Integration
{
	public class WhenAnErrorOccurs
	{
		BlitlineApi _blitline = default(BlitlineApi);
		BlitlineResponse _response = default(BlitlineResponse);
		BlitlineRequest _request = default(BlitlineRequest);

		[Specification]
		public void ShouldGenerateAnError()
		{
			"Give I have an incomplete request".Context(() =>
				{
					_blitline = new BlitlineApi();
					_request = new BlitlineRequest("blah", "https://s3-eu-west-1.amazonaws.com/gdoubleu-blitline/moi.jpg");

					var cropFunction = new CropFunction
					{
						X = 51,
						Y = 126,
						Width = 457 - 126,
						Height = 382 - 51,
						Save = new Save
						{
							ImageIdentifier = "image_identifier"
						}
					};

					_request.AddFunction(cropFunction);
				});

			"When I process the request".Do(() => _response = _blitline.ProcessImages(_request));

			"Then the response should fail".Observation(() => Assert.True(_response.Failed));
			"And the response should contain an error".Observation(() => Assert.NotEmpty(_response.Results.Error));
		}

	}
}
