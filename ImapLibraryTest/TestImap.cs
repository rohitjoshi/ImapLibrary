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
							Console.Write("Host[]:");
							string sHost = Console.ReadLine();
							if (sHost.Length < 1)
								sHost = "pots.dummydomain.com";
							Console.Write("User[]:");
							string sUser = Console.ReadLine();
							if (sUser.Length < 1)
								sUser = "rjoshi";
							Console.Write("Password []:");
							string sPwd = Console.ReadLine();
							if (sPwd.Length < 1)
								sPwd = "";
							Console.WriteLine("########################################");
							Console.WriteLine("Host:{0}", sHost);
							Console.WriteLine("User:{0}", sUser);
							Console.WriteLine("########################################");
							oImap.Login(sHost, sUser, sPwd);
							

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
							bool bFetchBody = false;
							if (sFetchBody.ToLower() == "true")
								bFetchBody = true;
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
	}
}
