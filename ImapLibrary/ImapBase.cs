using System;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.IO;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Joshi.Utils.Imap
{
	public class ImapBase
	{
		// IMAP Message Flags
		/// <summary>
		/// Message flag:seen
		/// </summary>
		protected const string    IMAP_MSG_FLAG_SEEN = "seen";
		/// <summary>
		/// Message flag: answered
		/// </summary>
		protected const string    IMAP_MSG_FLAG_ANSWERED = "answered";
		/// <summary>
		/// Message flag: flagged
		/// </summary>
		protected const string    IMAP_MSG_FLAG_FLAGGED = "flagged";
		/// <summary>
		/// Message flag: draft
		/// </summary>
		protected const string    IMAP_MSG_FLAG_DRAFT = "draft";
		/// <summary>
		/// Message flag: deleted
		/// </summary>
		protected const string    IMAP_MSG_FLAG_DELETED = "deleted";
		/// <summary>
		/// Max Message flags: 10
		/// </summary>
		protected const ushort    IMAP_MAX_MSG_FLAGS = 10;
		/// <summary>
		/// Imap default port: 143
		/// </summary>
		protected const ushort    IMAP_DEFAULT_PORT = 143;
		/// <summary>
		/// Imap default timeout:30 sec
		/// </summary>
		protected const ushort    IMAP_DEFAULT_TIMEOUT = 30;
		/// <summary>
		/// Imap Command Identifier value:Initial 0
		/// </summary>
		protected static ushort   IMAP_COMMAND_VAL = 0;
		/// <summary>
		/// Imap command Identified prefix: IMAP00
		/// </summary>
		protected const string    IMAP_COMMAND_PREFIX = "IMAP00";
		/// <summary>
		/// Imap command identified which is combination of
		/// Imap identifier prefix and val
		/// eg. Prefix:IMAP00, Val: 1
		/// Imap command Identified= IMAP001
		/// </summary>
		protected string IMAP_COMMAND_IDENTIFIER 
		{
			get
			{ 
				return IMAP_COMMAND_PREFIX + IMAP_COMMAND_VAL.ToString()+ " ";
			}

		}
		/// <summary>
		/// Imap Server OK response which is combination of 
		/// Imap Identifier and Imap OK response.
		/// eg. IMAP001 OK
		/// </summary>
		protected string IMAP_SERVER_RESPONSE_OK 
		{
			get
			{
				return IMAP_COMMAND_IDENTIFIER + IMAP_OK_RESPONSE;
			}
		}
		/// <summary>
		/// Imap Server NO response which is combination of 
		/// Imap Identifier and Imap NO response.
		/// eg. IMAP001 NO
		/// </summary>
		protected string IMAP_SERVER_RESPONSE_NO
		{
			get
			{
				return IMAP_COMMAND_IDENTIFIER + IMAP_NO_RESPONSE;
			}
		}
		/// <summary>
		/// Imap Server BAD response which is combination of
		/// Imap Identifier and Imap BAD response.
		/// eg. IMAP001 BAD
		/// </summary>
		protected string IMAP_SERVER_RESPONSE_BAD 
		{
			get
			{
				return IMAP_COMMAND_IDENTIFIER + IMAP_BAD_RESPONSE;
			}
		}

		/// <summary>
		/// Imap Untagged response prefix: *
		/// </summary>
		protected const string    IMAP_UNTAGGED_RESPONSE_PREFIX = "*";
		/// <summary>
		/// Impa ok response : OK
		/// </summary>
		protected const string    IMAP_OK_RESPONSE = "OK";
		/// <summary>
		/// Imap no response: NO
		/// </summary>
		protected const string    IMAP_NO_RESPONSE = "NO";
		/// <summary>
		/// Imap bad response :BAD
		/// </summary>
		protected const string    IMAP_BAD_RESPONSE = "BAD";
		/// <summary>
		/// Imap bad server response : "* BAD"
		/// </summary>
		protected const string    IMAP_BAD_SERVER_RESPONSE = "* BAD";
		/// <summary>
		/// Imap ok server response: "* OK"
		/// </summary>
		protected const string    IMAP_OK_SERVER_RESPONSE  = "* OK";
		/// <summary>
		/// Imap capability command : CAPABILITY
		/// </summary>
		protected const string    IMAP_CAPABILITY_COMMAND = "CAPABILITY";
		/// <summary>
		/// Imap connect command :CONNECT
		/// </summary>
		protected const string    IMAP_CONNECT_COMMAND = "CONNECT";
		/// <summary>
		/// Imap login command : LOGIN userid  password
		/// </summary>
		protected const string    IMAP_LOGIN_COMMAND = "LOGIN";
		/// <summary>
		/// Imap logout command : LOGOUT
		/// </summary>
		protected const string    IMAP_LOGOUT_COMMAND = "LOGOUT";
		/// <summary>
		/// Imap select command : SELECT INBOX
		/// </summary>
		protected const string    IMAP_SELECT_COMMAND = "SELECT";
		/// <summary>
		/// Imap examine command : EXAMINE INBOX
		/// </summary>
		protected const string    IMAP_EXAMINE_COMMAND = "EXAMINE";
		/// <summary>
		/// Imap append command : APPEND
		/// </summary>
		protected const string    IMAP_APPEND_COMMAND = "APPEND";
		/// <summary>
		/// Imap quota response : QUOTA
		/// </summary>
		protected const string    IMAP_QUOTA_RESPONSE = "QUOTA"; 
		/// <summary>
		/// Imap get quota command : GETQUOTAROOT 
		/// </summary>
		protected const string    IMAP_GETQUOTA_COMMAND = "GETQUOTAROOT";
		/// <summary>
		/// Imap append response start : [
		/// </summary>
		protected const char      IMAP_APPEND_RESPONSE_START = '[';
		/// <summary>
		/// Imap append response end : ]
		/// </summary>
		protected const char      IMAP_APPEND_RESPONSE_END = ']';
		/// <summary>
		/// Imap go ahead response: +
		/// </summary>
		protected const char      IMAP_GO_AHEAD_RESPONSE = '+';
		/// <summary>
		/// Imap uid search command : UID SEARCH
		/// </summary>
		protected const string    IMAP_SEARCH_COMMAND = "UID SEARCH";
		/// <summary>
		/// Imap search command : SEARCH
		/// </summary>
		protected const string    IMAP_SEARCH_RESPONSE = "SEARCH";
		/// <summary>
		/// Imap uid fetch command : UID FETCH
		/// </summary>
		protected const string    IMAP_UIDFETCH_COMMAND = "UID FETCH";
		/// <summary>
		/// Imap fetch command : FETCH
		/// </summary>
		protected const string    IMAP_FETCH_COMMAND = "FETCH";
		/// <summary>
		/// Imap BodyStructure command
		/// </summary>
		protected const string    IMAP_BODYSTRUCTURE_COMMAND = "BODYSTRUCTURE";
		/// <summary>
		/// Imap uid store command
		/// </summary>
		protected const string    IMAP_UIDSTORE_COMMAND = "UID STORE";

        /// <summary>
        /// Imap uid copy command
        /// </summary>
        protected const string IMAP_UIDCOPY_COMMAND = "UID COPY";
		/// <summary>
		/// Imap expunge command
		/// </summary>
		protected const string    IMAP_EXPUNGE_COMMAND = "EXPUNGE";
		/// <summary>
		/// Imap noop command : NOOP
		/// </summary>
		protected const string    IMAP_NOOP_COMMAND = "NOOP";

        /// <summary>
        /// Imap add flags +flags
        /// </summary>
        protected const string IMAP_SETFLAGS_COMMAND = "+FLAGS";

        /// <summary>
        /// Imap remove flags -flags
        /// </summary>
        protected const string IMAP_REMOVEFLAGS_COMMAND = "-FLAGS";
		/// <summary>
		/// Imap command terminator: \r\n
		/// </summary>
		protected const string    IMAP_COMMAND_EOL = "\r\n";
		/// <summary>
		/// Imap message nil size : NIL
		/// </summary>
		protected const string    IMAP_MESSAGE_NIL = "NIL";
		/// <summary>
		/// Imap message header terminator : \r\n
		/// </summary>
		protected const string    IMAP_MESSAGE_HEADER_EOL = "\r\n";
		/// <summary>
		/// Imap message size start : '{'
		/// </summary>
		protected const char      IMAP_MESSAGE_SIZE_START = '{';
		/// <summary>
		/// Imap message size end : '}'
		/// </summary>
		protected const char      IMAP_MESSAGE_SIZE_END = '}';
		/// <summary>
		/// Imap message content type : "Content-Type: "
		/// </summary>
		protected const string    IMAP_MESSAGE_CONTENT_TYPE   = "content-type";
		/// <summary>
		/// Imap mesage content type: rfc822
		/// </summary>
		protected const string    IMAP_MESSAGE_RFC822 = "message/rfc822";
		/// <summary>
		/// Imap message id
		/// </summary>
		protected const string    IMAP_MESSAGE_ID =  "message-id";
		/// <summary>
		/// Imap mesage content type: multipart
		/// </summary>
		protected const string    IMAP_MESSAGE_MULTIPART = "multipart";
		/// <summary>
		/// Imap content encoding : "Content-Transfer-Encoding: "
		/// </summary>
		protected const string    IMAP_MESSAGE_CONTENT_ENCODING  = "content-transfer-encoding";
		/// <summary>
		/// Imap content description : "Content-Description: "
		/// </summary>
		protected const string    IMAP_MESSAGE_CONTENT_DESC      = "content-description";
		/// <summary>
		/// Imap content disposition : "Content-Disposition: "
		/// </summary>
		protected const string    IMAP_MESSAGE_CONTENT_DISP      = "content-disposition";
		/// <summary>
		/// Imap content size
		/// </summary>
		protected const string    IMAP_MESSAGE_CONTENT_SIZE      = "content-size";
		/// <summary>
		/// Imap content lines
		/// </summary>
		protected const string    IMAP_MESSAGE_CONTENT_LINES     = "content-lines";

		/// <summary>
		/// Imap message base64 encoding : BASE64
		/// </summary>
		protected const string    IMAP_MESSAGE_BASE64_ENCODING   = "base64";
		/// <summary>
		/// Imap message default part : 1
		/// </summary>
		protected const string    IMAP_MSG_DEFAULT_PART          = "1";
		/// <summary>
		/// Imap header Sender tag
		/// </summary>
		protected const string   IMAP_HEADER_SENDER_TAG          = "sender";
        /// <summary>
        /// Imap header from tag
        /// </summary>
		protected const string   IMAP_HEADER_FROM_TAG            = "from";
		/// <summary>
		/// Imap header in-reply-to tag
		/// </summary>
		protected const string   IMAP_HEADER_IN_REPLY_TO_TAG     = "in-reply-to";
		/// <summary>
		/// IKmap header reply-to tag
		/// </summary>
		protected const string   IMAP_HEADER_REPLY_TO_TAG        = "reply-to";
		/// <summary>
		/// Imap header to tag
		/// </summary>
		protected const string   IMAP_HEADER_TO_TAG              = "to";
		/// <summary>
		/// Imap header cc tag
		/// </summary>
		protected const string   IMAP_HEADER_CC_TAG              = "cc";
		/// <summary>
		/// Imap header bcc tag
		/// </summary>
		protected const string   IMAP_HEADER_BCC_TAG             = "bcc";
		/// <summary>
		/// Imap header subject tag
		/// </summary>
		protected const string   IMAP_HEADER_SUBJECT_TAG         = "subject";
		/// <summary>
		/// Imap header date tag
		/// </summary>
		protected const string   IMAP_HEADER_DATE_TAG            = "date";
		/// <summary>
		/// Imap body type
		/// </summary>
		protected const string    IMAP_PLAIN_TEXT                = "text/plain";
		/// <summary>
		/// Imap audio wave:  audio/wav
		/// </summary>
		protected const string    IMAP_AUDIO_WAV                 = "audio/wav";
		/// <summary>
		/// Imap video mpeg4  : video/mpeg4
		/// </summary>
		protected const string    IMAP_VIDEO_MPEG4               = "video/mpeg4";
 
		/// <summary>
		/// Imap host
		/// </summary>
		protected string m_sHost = "";
		/// <summary>
		/// Imap port : default IMAP_DEFAULT_PORT : 143
		/// </summary>
		protected ushort m_nPort = IMAP_DEFAULT_PORT;
		/// <summary>
		/// User id
		/// </summary>
		protected string m_sUserId = "";
		/// <summary>
		/// User Password
		/// </summary>
		protected string m_sPassword = "";
        /// <summary>
        /// SSL Enabled
        /// </summary>
	    protected bool m_bSSLEnabled = false;
		/// <summary>
		/// Is Imap server connected
		/// </summary>
		protected bool   m_bIsConnected = false;
		/// <summary>
		/// Tcpclient object
		/// </summary>
		TcpClient m_oImapServ;
		/// <summary>
		/// Network stream object
		/// </summary>
		Stream m_oNetStrm;
		/// <summary>
		/// StreamReader object
		/// </summary>
		StreamReader  m_oRdStrm;

		/// <summary>
		/// Imap server response result
		/// </summary>
		protected enum ImapResponseEnum 
		{
			/// <summary>
			/// Imap Server responded "OK"
			/// </summary>
			IMAP_SUCCESS_RESPONSE,
			/// <summary>
			/// Imap Server responded "NO" or "BAD"
			/// </summary>
			IMAP_FAILURE_RESPONSE,
			/// <summary>
			/// Imap Server responded "*"
			/// </summary>
			IMAP_IGNORE_RESPONSE 
		}
		/// <summary>
		/// Log type enum
		/// </summary>
		protected enum LogTypeEnum 
		{
			/// <summary>
			/// Information
			/// </summary>
			INFO,
			/// <summary>
			/// Warning
			/// </summary>
			WARN,
			/// <summary>
			/// Error
			/// </summary>
			ERROR, 
			/// <summary>
			/// Imap Log information
			/// </summary>
			IMAP
		}
		/// <summary>
		/// Logging function
		/// </summary>
		/// <param name="type">Log type;LogTypeEnum</param>
		/// <param name="log">Log data</param>
		protected void Log (LogTypeEnum type, string log) 
		{
			switch (type) 
			{
				case LogTypeEnum.INFO:
					Console.WriteLine("Info:" + log);
					break;
				case LogTypeEnum.WARN:
					Console.WriteLine("Warning:" + log);
					break;
				case LogTypeEnum.ERROR:
					Console.WriteLine("Error:" + log);
					break;
				case LogTypeEnum.IMAP:
					Console.WriteLine(log);
					break;

			}
		}

	    /// <summary>
	    /// Connect to specified host and port
	    /// </summary>
	    /// <param name="sHost">Imap host</param>
	    /// <param name="nPort">Imap port</param>
	    /// <param name="sslEnabled"> </param>
	    /// <returns>ImapResponseEnum type</returns>
	    protected ImapResponseEnum Connect(string sHost, ushort nPort, bool sslEnabled=false) 
		{
			IMAP_COMMAND_VAL = 0;
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
			ImapException e_connect = new ImapException (ImapException.ImapErrorEnum.IMAP_ERR_CONNECT, sHost);
			try 
			{
				m_oImapServ = new TcpClient(sHost, nPort);
                if(sslEnabled)
                {
                   
                    var sslStrm = new SslStream(m_oImapServ.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
                    //m_SSLStream.ReadTimeout = 30000;

                    try
                    {
                        sslStrm.AuthenticateAsClient(sHost);
                    }
                    catch (AuthenticationException e)
                    {
                        Console.WriteLine("Exception: {0}", e.Message);
                        if (e.InnerException != null)
                        {
                            Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                        }
                        Console.WriteLine("Authentication failed - closing the connection.");
                        m_oImapServ.Close();
                        return ImapResponseEnum.IMAP_FAILURE_RESPONSE;
                    }

                    m_oNetStrm = (Stream) sslStrm;
                    m_oRdStrm = new StreamReader(sslStrm);
                }else
                {
                    m_oNetStrm = m_oImapServ.GetStream();
                    m_oRdStrm = new StreamReader(m_oImapServ.GetStream());
                }
				
				
				string sResult = m_oRdStrm.ReadLine();
				if (sResult.StartsWith(IMAP_OK_SERVER_RESPONSE)== true)
				{
					Log (LogTypeEnum.IMAP, sResult);
					eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
					Capability();
				}
				else
				{
					Log (LogTypeEnum.IMAP, sResult);
					eImapResponse = ImapResponseEnum.IMAP_FAILURE_RESPONSE;
				}
			}
			catch
			{
				throw e_connect;
			}
			m_sHost = sHost;	
			m_nPort = nPort;
	        m_bSSLEnabled = sslEnabled;
			return eImapResponse;
		}


		/// <summary>
		/// Disconnect connection with Imap server
		/// </summary>
		protected void Disconnect() 
		{
			IMAP_COMMAND_VAL = 0;
			if (m_bIsConnected)
			{
				if (m_oNetStrm != null)
					m_oNetStrm.Close();
				if (m_oRdStrm != null)
					m_oRdStrm.Close();	
			}
			
		}

		/// <summary>
		/// Send command to server and retrieve response
		/// </summary>
		/// <param name="command">Command to send Imap Server</param>
		/// <param name="sResultArray">Imap Server response</param>
		/// <returns>ImapResponseEnum type</returns>
		protected ImapResponseEnum SendAndReceive(string command, ref ArrayList sResultArray)
		{
			IMAP_COMMAND_VAL++;
			string sCommand = IMAP_COMMAND_IDENTIFIER + command;
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
			Log(LogTypeEnum.IMAP, sCommand.TrimEnd(IMAP_COMMAND_EOL.ToCharArray()));
			byte [] data = System.Text.Encoding.ASCII.GetBytes(sCommand.ToCharArray());
			try 
			{
				m_oNetStrm.Write(data, 0, data.Length);
				bool bRead = true;
				while (bRead)
				{
					string sResult = m_oRdStrm.ReadLine();
					sResultArray.Add(sResult);
					
					if (sResult.StartsWith(IMAP_SERVER_RESPONSE_OK))
					{
						bRead = false;
						Log (LogTypeEnum.IMAP, sResult);
						eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
					}
					else if (sResult.StartsWith(IMAP_SERVER_RESPONSE_NO))
					{
						bRead = false;
						Log (LogTypeEnum.IMAP, sResult);
						eImapResponse = ImapResponseEnum.IMAP_FAILURE_RESPONSE;
					}
					else if (sResult.StartsWith(IMAP_SERVER_RESPONSE_BAD))
					{
						bRead = false;
						Log (LogTypeEnum.IMAP, sResult);
						eImapResponse = ImapResponseEnum.IMAP_FAILURE_RESPONSE;
					}
					else Log (LogTypeEnum.IMAP, sResult);
				}
			}
			catch (Exception e)
			{
				//LogOut();
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_SERIOUS, e.Message);
				
			}
			Log(LogTypeEnum.IMAP,"");
			return eImapResponse;
		}

		/// <summary>
		///  retrieve response
		/// </summary>
		/// <param name="command">Command to send Imap Server</param>
		/// <param name="sResultArray">Imap Server response</param>
		/// <returns>ImapResponseEnum type</returns>
		protected ImapResponseEnum Receive(ref ArrayList sResultArray)
		{
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
			try 
			{
				bool bRead = true;
				while (bRead)
				{
					string sResult = m_oRdStrm.ReadLine();
					sResultArray.Add(sResult);
					
					if (sResult.StartsWith(IMAP_SERVER_RESPONSE_OK))
					{
						bRead = false;
						Log (LogTypeEnum.IMAP, sResult);
						eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
					}
					else if (sResult.StartsWith(IMAP_SERVER_RESPONSE_NO))
					{
						bRead = false;
						Log (LogTypeEnum.IMAP, sResult);
						eImapResponse = ImapResponseEnum.IMAP_FAILURE_RESPONSE;
					}
					else if (sResult.StartsWith(IMAP_SERVER_RESPONSE_BAD))
					{
						bRead = false;
						Log (LogTypeEnum.IMAP, sResult);
						eImapResponse = ImapResponseEnum.IMAP_FAILURE_RESPONSE;
					}
					else Log (LogTypeEnum.IMAP, sResult);
				}
			}
			catch (Exception e)
			{
				//LogOut();
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_SERIOUS, e.Message);
				
			}
			Log(LogTypeEnum.IMAP,"");
			return eImapResponse;
		}

		/// <summary>
		/// Send command to server and retrieve response
		/// </summary>
		/// <param name="command">Command to send Imap Server</param>
		/// <param name="sResultArray">Imap Server response</param>
		/// <returns>ImapResponseEnum type</returns>
		protected ImapResponseEnum SendAndReceiveByNumLines(string command, ref ArrayList sResultArray, int nNumLines)
		{
			IMAP_COMMAND_VAL++;
			int nLineCount = 0;
			string sCommand = IMAP_COMMAND_IDENTIFIER + command;
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
			Log(LogTypeEnum.IMAP, sCommand.TrimEnd(IMAP_COMMAND_EOL.ToCharArray()));
			byte [] data = System.Text.Encoding.ASCII.GetBytes(sCommand.ToCharArray());
			try 
			{
				m_oNetStrm.Write(data, 0, data.Length);
				bool bRead = true;
				while (bRead)
				{
					string sResult = m_oRdStrm.ReadLine();
					sResultArray.Add(sResult);
					nLineCount++;

					
					if (sResult.StartsWith(IMAP_SERVER_RESPONSE_OK) ||
						nLineCount == nNumLines)
					{
						bRead = false;
						Log (LogTypeEnum.IMAP, sResult);
						eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
					}
					else if (sResult.StartsWith(IMAP_SERVER_RESPONSE_NO))
					{
						bRead = false;
						Log (LogTypeEnum.IMAP, sResult);
						eImapResponse = ImapResponseEnum.IMAP_FAILURE_RESPONSE;
					}
					else if (sResult.StartsWith(IMAP_SERVER_RESPONSE_BAD))
					{
						bRead = false;
						Log (LogTypeEnum.IMAP, sResult);
						eImapResponse = ImapResponseEnum.IMAP_FAILURE_RESPONSE;
					}
					else Log (LogTypeEnum.IMAP, sResult);
				}
			}
			catch (Exception e)
			{
				//LogOut();
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_SERIOUS, e.Message);
				
			}
			Log(LogTypeEnum.IMAP,"");
			return eImapResponse;
		}
/// <summary>
/// Read the Server Response by specified size
/// </summary>
/// <param name="sBuffer"></param>
/// <param name="nSize"></param>
/// <returns></returns>
		protected int ReceiveBuffer (ref string sBuffer, int nSize)
		{
			int nRead = -1;
			char [] cBuff = new Char[nSize+1];
			//cBuff[nSize+1] = '\0';
			nRead = m_oRdStrm.Read(cBuff, 0, nSize);
			string sTmp = new String(cBuff);
			sBuffer = sTmp;
			Log (LogTypeEnum.IMAP, sBuffer);
			return nRead;
		}


		
		/// <summary>
		/// IMAP Capability command
		/// </summary>
		public void Capability()
		{
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
			ArrayList sResultArray = new ArrayList();
			string capabilityCommand = IMAP_CAPABILITY_COMMAND;
			capabilityCommand += IMAP_COMMAND_EOL;
			try
			{
				eImapResponse = SendAndReceive(capabilityCommand, ref sResultArray);
				if (eImapResponse != ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_CAPABILITY);
			}
			catch (Exception e)
			{
				throw e;
			}
		}
        /// <summary>
        /// Validate Certificate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        public bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Failed to validate Server Certificate. Error: {0}", sslPolicyErrors);

            return false;
        }

	

	}
}