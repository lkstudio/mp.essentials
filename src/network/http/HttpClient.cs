﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace VVVV.Nodes
{
    public enum SendMethod
    {
        Delete,
        Get,
        Head,
        Options,
        Post,
        Put,
        Trace
    }
    public class HttpClientContainer : IDisposable
    {
        public HttpClient Client = new HttpClient();
        public List<Task<HttpResponseMessage>> OngoingRequests = new List<Task<HttpResponseMessage>>();

        public HttpClientContainer() { }

        public Task<HttpResponseMessage> Send(HttpRequestMessage hrm, HttpCompletionOption hco)
        {
            Task<HttpResponseMessage> returntask = this.Client.SendAsync(hrm, hco);
            this.OngoingRequests.Add(returntask);
            return returntask;
        }

        public void Dispose()
        {
            this.OngoingRequests.Clear();
            this.Client.CancelPendingRequests();
            this.Client.Dispose();
        }
    }
}
