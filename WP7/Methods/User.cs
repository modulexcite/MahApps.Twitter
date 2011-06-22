﻿using System;
using System.Collections.Generic;
using System.Linq;
using Hammock.Web;
using MahApps.RESTBase;
using MahApps.Twitter.Delegates;
using MahApps.Twitter.Models;

namespace MahApps.Twitter.Methods
{
    public class Users : RestMethodsBase<IBaseTwitterClient>
    {
        private String baseAddress = "users/";
        public Users(IBaseTwitterClient context)
            : base(context)
        {

        }

        public void BeginSearch(string q, GenericResponseDelegate callback)
        {
            var p = new Dictionary<string, string> { { "q", q } };

            Context.BeginRequest(baseAddress + "search.json", p, WebMethod.Get, (req, res, state) =>
            {
                var obj = Context.Deserialise<ResultsWrapper<User>>(res.Content);
                if (callback != null)
                    callback(req, res, obj);
            });
        }

        public void BeginLookup(int[] userIds, GenericResponseDelegate callback)
        {
            string result = string.Join(",", userIds.Take(100).Select(x => x.ToString()).ToArray());
            var p = new Dictionary<string, string> { { "user_id", result } };

            Context.BeginRequest(baseAddress + "lookup.json", p, WebMethod.Post, (req, res, state) =>
            {
                var obj = Context.Deserialise<ResultsWrapper<User>>(res.Content);
                if (callback != null)
                    callback(req, res, obj);
            });
        }
    }
}
