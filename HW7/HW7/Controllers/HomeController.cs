using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HW7.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Info userInfo = GithubUserInfo(); 
            List<Repo> repoInfo = GithubRepoInfo();
            //string owner = "wou-cs";
            //string repo = "CS460-F19-sgreen18";
            //GithubCommitInfo(owner, repo);
            ////string userInfoContent = ;
            ViewBag.userInfo = userInfo;
            ViewBag.repoInfo = repoInfo;
            //return View(userInfo.Content, repoInfo.Content);
            return View();
        }
        public struct Info
        {
            public string user_login { get; set; }
            public string user_name { get; set; }
            public string user_email { get; set; }
            public string user_location { get; set; }
            public string user_company { get; set; }
            public string user_url { get; set; }
            public string user_imageUrl { get; set; }

            public List<Repo> user_repos { get; set; }
        }
        public struct Repo
        {
            public string repo_name { get; set; }
            public string repo_owner { get; set; }
            public string repo_image { get; set; }
            public string repo_url { get; set; }
            public DateTime repo_updated { get; set; }
        }
        public struct Commit
        {
            public string commit_sha;
            public string commit_url;
            public string commit_name;
            public DateTime commit_date;
            public string commit_message;
        }
        public JsonResult RandomNumbers(int? id = 100)
        {
            Random gen = new Random();
            var data = new
            {
                message = "Random numbers API",
                num = (int) id,
                numbers = Enumerable.Range(1, (int)id).Select(x => gen.Next(1000)),
                numbers1 = Enumerable.Range(1, (int)id)
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public Info GithubUserInfo()
        {
            string json = SendRequest("https://api.github.com/user");
            //parse json data, exception handling try catch
            JObject user = JObject.Parse(json);
            
            Info newUser = new Info();
            newUser.user_imageUrl = (string)user["avatar_url"];
            newUser.user_login = (string)user["login"];
            newUser.user_name = (string)user["name"];
            newUser.user_email = (string)user["email"];
            newUser.user_url = (string)user["html_url"];
            newUser.user_company = (string)user["company"];
            newUser.user_location = (string)user["location"];
            return newUser;
            //List<string> output = new List<string>();
            //var myuser = new
            //{
            //    login = login,
            //    name = name,
            //    email = email,
            //    url = url,
            //    company = company,
            //    location = location
            //};
            //return Json(myuser);
            //string o = $"\"login\": \"{login}\", \"name\": \"{name}\", \"url\": \"{url}\", \"email\": \"{email}\", \"company\": \"{company}\", \"location\": \"{location}\"";
            //return o;
            //output.Add();

            //var jsonArrayString = @"{'login': " + jlogin + ",'name': " + jname +",'email': " + jemail + ",'comapany': " + jcompany + ",'location': " + jlocation;

            //string jsonString = JsonConvert.SerializeObject(o, Formatting.Indented);
            //string jsonString = JsonConvert.SerializeObject(output, Formatting.Indented);

            //return new ContentResult 
            //{
            //    Content = jsonString,
            //    ContentType = "application/json",
            //    ContentEncoding = System.Text.Encoding.UTF8
            //};
        }

        public List<Repo> GithubRepoInfo()
        {
            //string json = SendRequest("https://api.github.com/users/sgreen/repos");
            string json = SendRequest("https://api.github.com/user/repos");
            JArray repos = JArray.Parse(json);

            //Info user = new Info();
            List<Repo> output = new List<Repo>();

            foreach (var repo in repos)
            {
                Repo myrepo = new Repo
                {
                    repo_name = (string)repo["name"],
                    repo_owner = (string)repo["owner"]["login"],
                    repo_image = (string)repo["owner"]["avatar_url"],
                    repo_url = (string)repo["html_url"],
                    repo_updated = (DateTime)repo["updated_at"]
                };
                
                //output.Add($"{name}: {owner}, {url}, {updated}");
                //output.Add($"{name}: {owner}, {url}");
                output.Add(myrepo);
            }

            //foreach (var repo in output) { user.user_repos.Add(repo); }
            return output;
            //string jsonString = JsonConvert.SerializeObject(output, Formatting.Indented);
            //return new ContentResult
            //{
            //    Content = jsonString,
            //    ContentType = "application/json",
            //    ContentEncoding = System.Text.Encoding.UTF8
            //};
        }

        [HttpGet]
        public ActionResult GithubCommitInfo(string owner, string repo)
        {
            string json = SendRequest("https://api.github.com/repos/" + owner + "/" + repo + "/commits");

            JArray commits = JArray.Parse(json);
            List<string> output = new List<string>();
            //List<Commit> output = new List<Commit>();

            foreach (var commit in commits)
            {
                //Commit mycommit = new Commit();
                string sha = (string)commit["sha"];
                string url = (string)commit["html_url"];
                string name = (string)commit["commit"]["author"]["name"];
                DateTime date = (DateTime)commit["commit"]["committer"]["date"];
                string message = (string)commit["commit"]["message"];
                //mycommit.commit_sha = (string)commit["sha"];
                //mycommit.commit_url = (string)commit["html_url"];
                //mycommit.commit_name = (string)commit["commit"]["committer"]["name"];
                //mycommit.commit_date = (DateTime)commit["commit"]["committer"]["date"];
                //mycommit.commit_message = (string)commit["commit"]["message"];

                //output.Add(mycommit);
                output.Add($"{{\"sha\": \"{sha}\", \"url\": \"{url}\", \"name\": \"{name}\", \"date\": \"{date}\", \"message\": \"{message}\"}}");
            }
            //return output;
            string jsonString = JsonConvert.SerializeObject(output, Formatting.Indented);

            return new ContentResult
            {
                Content = jsonString,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8
            };
        }

        private string SendRequest(string uri)
        {
            string credentials = System.Configuration.ConfigurationManager.AppSettings["CS460GithubAPIKey"];
            string username = System.Configuration.ConfigurationManager.AppSettings["CS460GithubUserName"];

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("Authorization", "token " + credentials);
            request.UserAgent = username;       // Required, see: https://developer.github.com/v3/#user-agent-required
            request.Accept = "application/json";

            string jsonString = null;
            // TODO: You should handle exceptions here
            using (     WebResponse response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
                reader.Close();
                stream.Close();
            }
            return jsonString;
        }
        
    }
}