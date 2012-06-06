using System;
using System.Collections;
using System.Xml;
using Joshi.Utils.Imap;


namespace ConsoleApplication1
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			
			
			Imap oImap = new Imap();
			bool bNotExit = true;
			while (bNotExit)
			{
				try 
				{
					Console.WriteLine("Select the action");
					Console.WriteLine("1    Login");
					Console.WriteLine("2    Select/Examine");
					Console.WriteLine("3    Search");
					Console.WriteLine("4    FetchHeader");
					Console.WriteLine("5    GetQuota");
					Console.WriteLine("6    Logout");
					Console.WriteLine("9    Exit");
					Console.Write("Input  :[9]");
					string sInput = Console.ReadLine();
                    int input = 0;
                    try {
                        input = Convert.ToInt32(sInput,10);
                    }catch(System.FormatException e) {
                        Console.WriteLine("Error:{0}:{1}",e.Message, e.InnerException);
                    }
					switch(input)
					{
						case 1:
						{
                            Console.Write("Host[imap.gmail.com]:");
							string sHost = Console.ReadLine();
							if (sHost.Length < 1)
								sHost = "imap.gmail.com";

                            Console.Write("Port [993]:");
                            string sPort = Console.ReadLine();
                            if (sPort.Length < 1)
                            {
                                sPort = "993";
                            }

                            Console.Write("SSLEnabled[True]:");
                            string sSslEnabled = Console.ReadLine();
						    bool bSSL = true;
                            if ( (sSslEnabled.Length < 1) || (String.Compare(sSslEnabled, "yes",true) == 0) || (String.Compare(sSslEnabled, "true",true) == 0) )
                            {

                                bSSL = true;
                            }else
                            {
                                bSSL = false;
                            }
							Console.Write("User[]:");
							string sUser = Console.ReadLine();
							if (sUser.Length < 1)
								sUser = "rjoshi";
							Console.Write("Password []:");
							string sPwd = ReadPassword();
							if (sPwd.Length < 1)
								sPwd = "";
                           
							Console.WriteLine("########################################");
							Console.WriteLine("Host:{0}", sHost);
                            Console.WriteLine("Port:{0}", sPort);
                            Console.WriteLine("SSLEnabled:{0}", bSSL.ToString());
							Console.WriteLine("User:{0}", sUser);
							Console.WriteLine("########################################");
                            oImap.Login(sHost, Convert.ToUInt16(sPort), sUser, sPwd, bSSL);
							

						}
							break;

						case 2:
						{
							Console.Write("Folder: [INBOX]");
							string sInbox = Console.ReadLine();
							Console.Write("Select:s Examine:e [s]");
							string sChoice = Console.ReadLine();
							if (sInbox.Length < 1 )
								sInbox = "INBOX";
							if (sChoice == "e")
								oImap.ExamineFolder(sInbox);
							else 
								oImap.SelectFolder(sInbox);
							
						}
							break;
						case 3:
						{
							Console.Write("Search String:");
							string sSearch = Console.ReadLine();
							string [] saSearchData = new String[1];
							saSearchData[0] = sSearch;
							ArrayList saArray = new ArrayList();
							oImap.SearchMessage(saSearchData,false,saArray);
							//c.Search(oImap, sSearch);
						}
							break;
						case 4:
						{
							Console.Write("Message UID[]:");
							string sUid = Console.ReadLine();
							if (sUid.Length < 1)
								sUid = "";
							Console.Write("Fetch Body:[false]");
							string sFetchBody = Console.ReadLine();
						    bool bFetchBody = sFetchBody.ToLower() == "true";
						    ArrayList saArray = new ArrayList();
							string sFileName = sUid + ".xml";
							XmlTextWriter oXmlWriter = new XmlTextWriter(sFileName,System.Text.Encoding.UTF8);
							oXmlWriter.Formatting = Formatting.Indented;
							oXmlWriter.WriteStartDocument(true);
							oXmlWriter.WriteStartElement("Message");
							oXmlWriter.WriteAttributeString("UID", sUid);
							oImap.FetchMessage(sUid, oXmlWriter, bFetchBody);
							oXmlWriter.WriteEndElement();
							oXmlWriter.WriteEndDocument();
							oXmlWriter.Flush();
							oXmlWriter.Close();
							//oImap.FetchPartHeader(sUid, sPart, saArray);
							//c.GetHeader(oImap, sUid, sPart);

						}
							break;
						case 5:
						{
							bool bUnlimitedQuota = false;
							int nUsedKBytes = 0;
							int nTotalKBytes = 0;
				
							oImap.GetQuota("inbox", ref bUnlimitedQuota, ref nUsedKBytes, ref nTotalKBytes);
							Console.WriteLine("Unlimitedquota:{0}, UsedKBytes:{1}, TotalKBytes:{2}",
								bUnlimitedQuota, nUsedKBytes, nTotalKBytes);
						}
							break;
						case 6:
							oImap.LogOut();
							break;
						case 9:
							oImap.LogOut();
							bNotExit = false;
							break;
					}
				}
				catch (ImapException e)
				{
					Console.WriteLine("Error:{0}:{1}", e.Message, e.InnerException);
				}
			}


		}
        /// <summary>
        /// Read password
        /// </summary>
        /// <returns></returns>
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }

            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }
	}
}
