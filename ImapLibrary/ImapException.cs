using System;
namespace Joshi.Utils.Imap
{
	/// <summary>
	/// Imap Exception class which implements Imap releted exceptions
	/// </summary>
	public class ImapException : ApplicationException 
	{
		/// <summary>
		/// Exception message string
		/// </summary>
		string message;
		/// <summary>
		/// enum for Imap exception errors
		/// </summary>
		public enum ImapErrorEnum 
		{
			/// <summary>
			/// failure parsing the url
			/// </summary>
			IMAP_ERR_URI,     
			/// <summary>
			/// invalid message uid in the url
			/// </summary>
			IMAP_ERR_MESSAGEUID,    
			/// <summary>
			/// invalid username/password in the url
			/// </summary>
			IMAP_ERR_AUTHFAILED,   
			/// <summary>
			/// failure connecting to imap server
			/// </summary>
			IMAP_ERR_CONNECT,   
			/// <summary>
			/// not connected to any IMAP
			/// </summary>
			IMAP_ERR_NOTCONNECTED, 
			/// <summary>
			/// failure logging into imap server
			/// </summary>
			IMAP_ERR_LOGIN, 
            /// <summary>
            /// failure to logout from imap server
            /// </summary>
			IMAP_ERR_LOGOUT,  
			/// <summary>
			/// not enough data to restore
			/// </summary>
			IMAP_ERR_INSUFFICIENT_DATA, 
			/// <summary>
			/// timeout while waiting for response
			/// </summary>
			IMAP_ERR_TIMEOUT,
			/// <summary>
			/// socket error while receiving
			/// </summary>
			IMAP_ERR_SOCKET,             
			/// <summary>
			/// failure getting the quota information
			/// </summary>
			IMAP_ERR_QUOTA,              
			/// <summary>
			/// failure selecting a IMAP folder
			/// </summary>
			IMAP_ERR_SELECT,             
			/// <summary>
			/// failure examining an IMAP folder
			/// </summary>
			IMAP_ERR_EXAMINE,            
			/// <summary>
			///  No folder is currently selected
			/// </summary>
			IMAP_ERR_NOTSELECTED,        
			/// <summary>
			/// failure to search
			/// </summary>
			IMAP_ERR_SEARCH,             
			/// <summary>
			/// failed to do exact match after search
			/// </summary>
			IMAP_ERR_SEARCH_EXACT,       
			/// <summary>
			/// unsupported search key
			/// </summary>
			IMAP_ERR_INVALIDSEARCHKEY,   
			/// <summary>
			/// failure to get message MIME
			/// </summary>
			IMAP_ERR_GETMIME,            
			/// <summary>
			/// Message Header is in invalid format
			/// </summary>
			IMAP_ERR_INVALIDHEADER,      
			/// <summary>
			/// Failed to fetch the bodystructure
			/// </summary>
			IMAP_ERR_FETCHBODYSTRUCT,    
			/// <summary>
			/// failure to fetch a IMAP message
			/// </summary>
			IMAP_ERR_FETCHMSG,
            /// <summary>
            /// failure to fetch a IMAP message size
            /// </summary>
            IMAP_ERR_FETCHSIZE,
			/// <summary>
			/// failure to allocate memory
			/// </summary>
			IMAP_ERR_MEMALLOC,           
			/// <summary>
			/// failure to encode the audio content
			/// </summary>
			IMAP_ERR_ENCODINGERROR,      
			/// <summary>
			/// failure to read/write the audio content
			/// </summary>
			IMAP_ERR_FILEIO,             
			/// <summary>
			/// failure to store the message in IMAP
			/// </summary>
			IMAP_ERR_STOREMSG,           
			/// <summary>
			/// failure to issue expunge command
			/// </summary>
			IMAP_ERR_EXPUNGE,            
			/// <summary>
			/// invalid parameter to API
			/// </summary>
			IMAP_ERR_INVALIDPARAM,      
			/// <summary>
			/// Capability command error
			/// </summary>
			IMAP_ERR_CAPABILITY,
			/// <summary>
			/// Serious Problem
			/// </summary>
			IMAP_ERR_SERIOUS             
    
		}
		/// <summary>
		/// Property Message (string)
		/// </summary>
		public override string Message 
		{
			get 
			{
				return message;
			}
			
		}
		/// <summary>
		/// Error Type: ImapErrorEnum
		/// </summary>
        private ImapErrorEnum errorType;
		/// <summary>
		/// Property : Type (ImapErrorEnum)
		/// </summary>
		public ImapErrorEnum Type
		{
			get
			{
				return errorType;
			}
			set
			{
				errorType = value;
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">string</param>
		public ImapException (String message) : base (message) 
		{
			this.message = message;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">string</param>
		/// <param name="inner">Exception</param>
		public ImapException (String message, Exception inner) : base(message,inner) 
		{
			this.message = message;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="Type">ImapErrorEnum</param>
		public ImapException(ImapErrorEnum Type) 
		{
			errorType = Type;
			message = GetDescription(Type);
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="Type">ImapErrorEnum</param>
		/// <param name="error">string</param>
		public ImapException(ImapErrorEnum Type, string error) 
		{
			errorType = Type;
			message = GetDescription(Type);
			message = message + " " + error;
		}
		
		/// <summary>
		/// Get Description for specified Type
		/// </summary>
		/// <param name="Type">ImapErrorEnum type</param>
		/// <returns>string</returns>
		private string GetDescription(ImapErrorEnum Type) 
		{
			switch (Type) 
			{
				case ImapErrorEnum.IMAP_ERR_URI:
					return "Failure parsing the IMAP URL.";
				case ImapErrorEnum.IMAP_ERR_MESSAGEUID:
					return "Invalid UserName/Password in the IMAP URL.";
				case ImapErrorEnum.IMAP_ERR_CONNECT:
					return "Failure connecting to the IMAP server.";
				case ImapErrorEnum.IMAP_ERR_NOTCONNECTED:
					return "Not connected to any IMAP server. Do Login first.";
				case ImapErrorEnum.IMAP_ERR_LOGIN:
					return "Failed authenticating the user/password in the IMAP server.";
				case ImapErrorEnum.IMAP_ERR_INSUFFICIENT_DATA:
					return "Not enough data to Restore the connection.";
				case ImapErrorEnum.IMAP_ERR_LOGOUT:
					return "Failed to Logout from IMAP server.";
				case ImapErrorEnum.IMAP_ERR_TIMEOUT:
					return "Timeout while waiting for response from the IMAP server.";
				case ImapErrorEnum.IMAP_ERR_SOCKET:
					return "Socket Error. Failed receiving message.";
				case ImapErrorEnum.IMAP_ERR_QUOTA:
					return "Failure getting the quota information for the folder/mailbox.";
				case ImapErrorEnum.IMAP_ERR_SELECT:
					return "Failure selecting IMAP folder/mailbox.";
				case ImapErrorEnum.IMAP_ERR_NOTSELECTED:
					return "No Folder is currently selected.";
				case ImapErrorEnum.IMAP_ERR_EXAMINE:
					return "Failure examining IMAP folder/mailbox.";
				case ImapErrorEnum.IMAP_ERR_SEARCH:
					return "Failure searching IMAP with the given criteria.";
				case ImapErrorEnum.IMAP_ERR_SEARCH_EXACT:
					return "Failure getting the exact search value.";
				case ImapErrorEnum.IMAP_ERR_INVALIDSEARCHKEY:
					return "Unsupported search key passed to SearchMessage API.";
				case ImapErrorEnum.IMAP_ERR_GETMIME:
					return "Failure fetching mime for the message.";
				case ImapErrorEnum.IMAP_ERR_FETCHMSG:    
					return "Failure fetching message from IMAP folder/mailbox.";
                case ImapErrorEnum.IMAP_ERR_FETCHSIZE:
                    return "Failure fetching message size from IMAP folder/mailbox.";
				case ImapErrorEnum.IMAP_ERR_MEMALLOC:    
					return "Failure allocating memory.";
				case ImapErrorEnum.IMAP_ERR_ENCODINGERROR:    
					return "Failure encoding audio message.";
				case ImapErrorEnum.IMAP_ERR_FILEIO:    
					return "Failure reading/writing the audio content to file.";
				case ImapErrorEnum.IMAP_ERR_STOREMSG:    
					return "Failure storing message in IMAP.";
				case ImapErrorEnum.IMAP_ERR_EXPUNGE:    
					return "Failure permanently deleting marked messages (EXPUNGE).";
				case ImapErrorEnum.IMAP_ERR_INVALIDPARAM:    
					return "Invalid parameter passed to API. See Log for details";
				case ImapErrorEnum.IMAP_ERR_CAPABILITY:
					return "Failure capability command.";
				case ImapErrorEnum.IMAP_ERR_SERIOUS:    
					return "Serious Problem with IMAP. Contact System Admin.";
				case ImapErrorEnum.IMAP_ERR_FETCHBODYSTRUCT:
					return "Failure bodystructure command";
				default:
					throw new Exception("UnKnow Exception");
				
			}
			
		}
	}
}