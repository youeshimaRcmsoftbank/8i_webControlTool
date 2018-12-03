using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZigController.Models;
using System.IO;

namespace ZigController.Controllers
{
    public class HomeController : Controller
    {
        public class RequestPrm
        {
            public string command { get; set; }

        }

        public class Results
        {
            public string Cam1 { get; set; }
            public string Cam2 { get; set; }
            public string Cam3 { get; set; }
            public string Cam4 { get; set; }
            public string Cam5 { get; set; }
            public string Cam6 { get; set; }
            public string Cam7 { get; set; }
            public string Cam8 { get; set; }
            public string Cam9 { get; set; }
            public string Cam10 { get; set; }
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("")]
        public IActionResult DoPost()
        {
            Console.WriteLine("Request Comes");

            Microsoft.Extensions.Primitives.StringValues comm = "";
            Request.Form.TryGetValue("command", out comm);

            RequestPrm frm = new RequestPrm();
            frm.command = comm[0];

            Results r = new Results();
            r.Cam1 = "healthy";
            r.Cam2 = "healthy";
            r.Cam3 = "healthy";
            r.Cam4 = "healthy";
            r.Cam5 = "healthy";
            r.Cam6 = "healthy";
            r.Cam7 = "healthy";
            r.Cam8 = "healthy";
            r.Cam9 = "healthy";
            r.Cam10 = "healthy";

            Console.WriteLine("command is " + frm.command);
            if (frm.command == "1")
            {
                Console.WriteLine("storage command execute");

                string command = "/home/pi/8i/zig/scripts/zigcommand.py";
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.FileName = command;
                ps.Arguments = "master pushstoragemode 1 --pprint --verbose";
                ps.RedirectStandardOutput = true;
                ps.UseShellExecute = false;
                ps.WorkingDirectory = "/home/pi/";
                Process p = new Process();
                p.StartInfo = ps;
                string result = "";

                Console.WriteLine("exec storagecommand and waiting...");
                try
                {
                    p.Start();
                    p.WaitForExit();
                    result = p.StandardOutput.ReadToEnd();
                    p.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return BadRequest();
                }
                /*
                               System.IO.StreamReader sr = new StreamReader("storageResult.txt", System.Text.Encoding.UTF8);
                                result = sr.ReadToEnd();
                                sr.Close();
                                */

                Console.WriteLine("command execute was finished.. this is resuluts:" + result);
                Console.WriteLine("Analyzing result...");

                string[] resultArr = result.Split("\n");
                string json = "";
                string txt = "";
                for (int i = 0; i < resultArr.Length; i++)
                {
                    txt = resultArr[i];
                    if (txt == "")
                    {
                        continue;
                    }
                    if (txt.IndexOf("--->") != -1)
                    {
                        continue;
                    }
                    if (txt == "{")
                    {
                        while (true)
                        {
                            txt = resultArr[i];
                            json += txt;
                            if (txt == "}")
                            {
                                break;
                            }
                            i++;
                        }
                        break;  
                    }
                }

                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
                if (res.R01.success == "true")
                {
                    r.Cam1 = "healthy";
                }
                else
                {
                    r.Cam1 = "unhealthy";
                }

                if (res.R02.success == "true")
                {
                    r.Cam2 = "healthy";
                }
                else
                {
                    r.Cam2 = "unhealthy";
                }


                if (res.R03.success == "true")
                {
                    r.Cam3 = "healthy";
                }
                else
                {
                    r.Cam3 = "unhealthy";
                }


                if (res.R04.success == "true")
                {
                    r.Cam4 = "healthy";
                }
                else
                {
                    r.Cam4 = "unhealthy";
                }


                if (res.R05.success == "true")
                {
                    r.Cam5 = "healthy";
                }
                else
                {
                    r.Cam5 = "unhealthy";
                }


                if (res.R06.success == "true")
                {
                    r.Cam6 = "healthy";
                }
                else
                {
                    r.Cam6 = "unhealthy";
                }


                if (res.R07.success == "true")
                {
                    r.Cam7 = "healthy";
                }
                else
                {
                    r.Cam7 = "unhealthy";
                }


                if (res.R08.success == "true")
                {
                    r.Cam8 = "healthy";
                }
                else
                {
                    r.Cam8 = "unhealthy";
                }


                if (res.R09.success == "true")
                {
                    r.Cam9 = "healthy";
                }
                else
                {
                    r.Cam9 = "unhealthy";
                }


                if (res.R10.success == "true")
                {
                    r.Cam10 = "healthy";
                }
                else
                {
                    r.Cam10 = "unhealthy";
                }
            }
            else if ( frm.command == "2")
            {
                string command = "/home/pi/8i/zig/scripts/zigcommand.py";
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.FileName = command;
                ps.Arguments = "master pushstoragemode 0 --pprint --verbose togglepower";
                ps.RedirectStandardOutput = true;
                ps.UseShellExecute = false;
                ps.WorkingDirectory = "/home/pi/";
                Process p = new Process();
                p.StartInfo = ps;

                Console.WriteLine("storageoff command executed..waiting for response...");
                p.Start();
                p.WaitForExit();

                string result = p.StandardOutput.ReadToEnd();
                p.Close();

                Console.WriteLine("storageoff command executed was finished.. here is result:" + result);
                /*
                System.IO.StreamReader sr = new StreamReader("storageoffCommand.txt", System.Text.Encoding.UTF8);
                string result = sr.ReadToEnd();
                sr.Close();
                */
                string[] resultArr = result.Split("\n");
                string json = "";
                string txt = "";
                for (int i = 0; i < resultArr.Length; i++)
                {
                    txt = resultArr[i];
                    if (txt == "")
                    {
                        continue;
                    }
                    if (txt.IndexOf("--->") != -1)
                    {
                        continue;
                    }
                    if (txt == "{")
                    {
                        while (true)
                        {
                            txt = resultArr[i];
                            json += txt;
                            if (txt == "}")
                            {
                                break;
                            }
                            i++;
                        }
                        break;
                    }
                }

                var res = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
                if (res.R01.success == "true")
                {
                    r.Cam1 = "shootingmode_ok";
                }
                else
                {
                    r.Cam1 = "shootingmode_ng";
                }

                if (res.R02.success == "true")
                {
                    r.Cam2 = "shootingmode_ok";
                }
                else
                {
                    r.Cam2 = "shootingmode_ng";
                }


                if (res.R03.success == "true")
                {
                    r.Cam3 = "shootingmode_ok";
                }
                else
                {
                    r.Cam3 = "shootingmode_ng";
                }


                if (res.R04.success == "true")
                {
                    r.Cam4 = "shootingmode_ok";
                }
                else
                {
                    r.Cam4 = "shootingmode_ng";
                }


                if (res.R05.success == "true")
                {
                    r.Cam5 = "shootingmode_ok";
                }
                else
                {
                    r.Cam5 = "shootingmode_ng";
                }


                if (res.R06.success == "true")
                {
                    r.Cam6 = "shootingmode_ok";
                }
                else
                {
                    r.Cam6 = "shootingmode_ng";
                }


                if (res.R07.success == "true")
                {
                    r.Cam7 = "shootingmode_ok";
                }
                else
                {
                    r.Cam7 = "shootingmode_ng";
                }


                if (res.R08.success == "true")
                {
                    r.Cam8 = "shootingmode_ok";
                }
                else
                {
                    r.Cam8 = "shootingmode_ng";
                }


                if (res.R09.success == "true")
                {
                    r.Cam9 = "shootingmode_ok";
                }
                else
                {
                    r.Cam9 = "shootingmode_ng";
                }


                if (res.R10.success == "true")
                {
                    r.Cam10 = "shootingmode_ok";
                }
                else
                {
                    r.Cam10 = "shootingmode_ng";
                }


            }
            else
            {
                return BadRequest();
            }

            Console.WriteLine("Response to client");
            return Ok(r);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
