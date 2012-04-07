﻿using System.Net;
using Machine.Fakes;
using Machine.Specifications;

namespace Resticle.Specifications
{
    public class GetRestRequestSpecification
    {
        [Subject(typeof(GetRestRequest))]
        public class when_building_web_request : with_get_request
        {
            Because of = () =>
                webRequest = request.BuildWebRequest(transmission);

            It should_set_url = () =>
                request.Resource.Path.ShouldEqual("http://example.com/companies");

            It should_set_request_to_get_request = () =>
                webRequest.Method.ShouldEqual("GET");

            It should_set_content_length_to_zero = () =>
                webRequest.ContentLength.ShouldEqual(0);

            static WebRequest webRequest;
        }

        [Subject(typeof(GetRestRequest))]
        public class when_building_web_request_with_parameters : with_transmission
        {
            Establish context = () =>
            {
                var resource = new Resource("http://example.com/companies");
                resource.AddParameter("filter", "ftse");
                resource.AddParameter("starred", true);

                request = new GetRestRequest(resource);
            };

            Because of = () =>
                webRequest = request.BuildWebRequest(transmission);

            It should_set_url = () =>
                webRequest.RequestUri.ToString().ShouldEqual("http://example.com/companies?filter=ftse&starred=True");

            static WebRequest webRequest;

            static GetRestRequest request;
        }

        public class with_transmission : WithFakes
        {
            Establish context = () =>
                transmission = An<ITransmission>();

            protected static ITransmission transmission;
        }

        public class with_get_request : with_transmission
        {
            Establish context = () =>
                request = new GetRestRequest(new Resource("http://example.com/companies"));

            protected static GetRestRequest request;
        }
    }
}