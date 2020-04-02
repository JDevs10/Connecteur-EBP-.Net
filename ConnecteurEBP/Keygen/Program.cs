using Keygen.Classes;
using Keygen.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keygen
{
    class Program
    {
        static void Main(string[] args)
        {
            // add introduction
            new Utils().Intro();

            // ask if it's a trial key or prod key with or without limited time
            Console.WriteLine("Welsome to Sage Keygen !!!");
            
            bool globalReply = true;

            do
            {
                string key = "";
                string keyType = "";
                string reply = null;
                do
                {
                    Console.WriteLine("What type of key are you looking for ?\nTrial key (enter 't') or Production key (enter 'p')....");
                    Console.WriteLine("");
                    reply = Console.ReadLine();

                    if (reply.Equals("exit") || reply.Equals("end"))
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Thank you for using Keygen! ");
                        Console.WriteLine("Good Bye..... ");
                        System.Threading.Thread.Sleep(2000);
                        Environment.Exit(0);
                    }
                    else if (reply == "t" || reply == "p")
                    {
                        // good, ok
                        Console.WriteLine("");
                    }
                    else
                    {
                        // did not understand
                        Console.WriteLine("");
                        Console.WriteLine("Sorry I did not understand your anwser :/");
                        reply = null;
                    }
                } while (reply == null);

                //trial key
                if (reply.Equals("t"))
                {
                    Console.WriteLine("");
                    Console.WriteLine("Trial key it is !...");
                    keyType = "Trial";

                    string time_str = null;

                    // did not understand
                    do
                    {
                        Console.WriteLine("How long the trial will last ?\nOne day (enter '1'), one week (enter '7') or one month (enter '30') ...");
                        time_str = Console.ReadLine();

                        if (time_str.Equals("exit") || time_str.Equals("end"))
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Thank you for using Keygen! ");
                            Console.WriteLine("Good Bye..... ");
                            System.Threading.Thread.Sleep(2000);
                            Environment.Exit(0);
                        }

                        // did not understand
                        for (int i = 0; i < time_str.Length; i++)
                        {
                            if (!Char.IsNumber(time_str, i))
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Sorry I did not understand your anwser :/");
                                time_str = null;
                                break;
                            }
                        }
                    } while (time_str == null);

                    int time = Convert.ToInt16(time_str);

                    // generate the key with the time
                    GenererCle newKey = new GenererCle(time);
                    string result = newKey.generateKey();
                    if (result != null && !result.Equals("Done"))
                    {
                        //key sucessfuly generated
                        Console.WriteLine("");
                        Console.WriteLine(result);
                        key = result;
                    }
                    else
                    {
                        //problem will generating the key
                        Console.WriteLine("");
                        Console.WriteLine(result);
                    }
                }

                //production key
                if (reply.Equals("p"))
                {
                    Console.WriteLine("");
                    Console.WriteLine("Producttion key it is !...");
                    keyType = "Production";

                    string time_str = null;

                    // did not understand
                    do
                    {
                        Console.WriteLine("How long the production key will last ?\nMust be more than 31 days (enter any number) ...");
                        time_str = Console.ReadLine();

                        if (time_str.Equals("exit") || time_str.Equals("end"))
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Thank you for using Keygen! ");
                            Console.WriteLine("Good Bye..... ");
                            System.Threading.Thread.Sleep(2000);
                            Environment.Exit(0);
                        }

                        // did not understand
                        for (int i = 0; i < time_str.Length; i++)
                        {
                            if (!Char.IsNumber(time_str, i))
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Sorry I did not understand your anwser :/");
                                time_str = null;
                                break;
                            }
                        }
                    } while (time_str == null);

                    int time = Convert.ToInt16(time_str);

                    // generate the key with the time
                    GenererCle newKey = new GenererCle(time);
                    string result = newKey.generateKey();
                    if (result != null && !result.Equals("Done"))
                    {
                        //key sucessfuly generated
                        Console.WriteLine("");
                        Console.WriteLine(result);
                        key = result;
                    }
                    else
                    {
                        //problem will generating the key
                        Console.WriteLine("");
                        Console.WriteLine(result);
                    }
                }


                //generate key file
                string replyFile_ = null;
                do
                {
                    Console.WriteLine("");
                    Console.WriteLine("Do you want to save the key in a file ?");
                    Console.WriteLine("");
                    replyFile_ = Console.ReadLine();

                    if (replyFile_ == "y" || replyFile_ == "yes")
                    {
                        // good, ok
                        Console.WriteLine("");
                        Console.WriteLine("...");
                        System.Threading.Thread.Sleep(1000);

                        if (GenererCle.decrypter(key))
                        {
                            Random random = new Random();
                            char[] tab0 = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                            string value0 = tab0[random.Next(0, 9)] + "" + tab0[random.Next(0, 9)] + "" + tab0[random.Next(0, 9)] + "" + tab0[random.Next(0, 9)] + "" + tab0[random.Next(0, 9)];

                            ValidationKey key_ = new ValidationKey("1.0", keyType, "Big Data Consulting", value0 + "" + key, value0, "252564541856412515418924525155124651");
                            key_.saveInfo(key_, "key.key");

                            Console.WriteLine("");
                            Console.WriteLine("Key file created at " + key_.pathModule + @"\key.key");
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Error decryption faild!");
                        }
                    }
                    else if (replyFile_ == "n" || replyFile_ == "no" || replyFile_.Equals("exit") || replyFile_.Equals("end"))
                    {
                        // good, ok
                        Console.WriteLine("");
                        Console.WriteLine("Thank you for using Keygen! ");
                        Console.WriteLine("Good Bye..... ");
                        System.Threading.Thread.Sleep(1000);
                        globalReply = false;
                    }
                    else
                    {
                        // did not understand
                        Console.WriteLine("");
                        Console.WriteLine("Sorry I did not understand your anwser :/");
                        replyFile_ = null;
                    }
                } while (replyFile_ == null);



                string endReply_ = null;
                do
                {
                    Console.WriteLine("");
                    Console.WriteLine("Do you want to generate a new key ?");
                    Console.WriteLine("");
                    endReply_ = Console.ReadLine();

                    if (endReply_ == "y" || endReply_ == "yes")
                    {
                        // good, ok
                        Console.WriteLine("");
                        Console.WriteLine("Ok let's go...");
                        System.Threading.Thread.Sleep(1000);
                        globalReply = true;
                    }
                    else if (endReply_ == "n" || endReply_ == "no" || endReply_.Equals("exit") || endReply_.Equals("end"))
                    {
                        // good, ok
                        Console.WriteLine("");
                        Console.WriteLine("Thank you for using Keygen! ");
                        Console.WriteLine("Good Bye..... ");
                        System.Threading.Thread.Sleep(1000);
                        globalReply = false;
                    }
                    else
                    {
                        // did not understand
                        Console.WriteLine("");
                        Console.WriteLine("Sorry I did not understand your anwser :/");
                        endReply_ = null;
                    }
                } while (endReply_ == null);

                Console.WriteLine("");
            } while (globalReply);

        }
    }
}
