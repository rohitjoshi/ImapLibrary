ImapLibrary
===========

ImapLibrary written in C#

Introduction

The Internet Message Access Protocol (IMAP) allows a client to access and manipulate electronic mail messages on a server. It includes operations for creating, deleting, and renaming mailboxes; checking for new messages; permanently removing messages; setting and clearing flags; [RFC-822] and [MIME-IMB] parsing; searching; and selective fetching of message attributes, texts, and portions thereof. For more information: here.

I have written an IMAP client library which allows basic functionalities like login, select/examine folder, search messages, fetch message (Header, Body), get storage quota, and logout.

This is my first application developed in C#, so don't expect too much in terms of efficiency. It demonstrates the use of sockets, XML writer, and user defined exception handling. Please feel free to modify and use this code.

The attached zip file contains three directories.

IMAP Library: It contains three source files.

ImapBase.cs: contains the IMAP commands related to string, and socket related functionality.
ImapException.cs: defines the user defined IMAP related error messages.
Imap.cs: IMAP client library functions. It has the following public functions:
Login: Login to IMAP server. It requires IMAP hostname, port, username, and password.
 Collapse | Copy Code
<COMMAND_PREFIX> LOGIN <USERID> <PASSWORD>\r\n
Logout: Logout and close the socket.
 Collapse | Copy Code
<COMMAND_PREFIX> LOGOUT\r\n
SelectFolder: It selects the folder. It requires folder name as parameter.
 Collapse | Copy Code
<COMMAND_PREFIX> SELECT <FOLDER>\r\n
ExamineFolder: It is similar to SelectFolder, but it does examine.
 Collapse | Copy Code
<COMMAND_PREFIX> EXAMINE <FOLDER>\r\n
GetQuota: Get the quota of the mailbox.
 Collapse | Copy Code
<COMMAND_PREFIX> GETQUOTAROOT <FOLDER>\r\n
SearchMessage: You can search the messages. It will return the UID of messages. E.g., From rjoshi.
 Collapse | Copy Code
<COMMAND_PREFIX> SEARCH <SEARCH STRING>\r\n
FetchMessage: It retrieves the complete message with attachments and writes into an XML file. The XML file will be generated in your current directory with file name as <MessageUID>.xml. You need to pass the XmlTextWriter object, message UID, and flag to fetch body.
 Collapse | Copy Code
<COMMAND_PREFIX> UID FETCH  <MSG UID> BODY[HEADER]
FetchPartBody: Fetch the body for a specific part. It requires message UID, part number as parameter.
FetchPartHeader: Fetch the header of message.
Documentation: HTML Documentation for IMAP Library generated using Visual Studio .NET.

IMAP Library test program: The IMAP test program allows users to test the following functionalities.

Login
Select/Examine folder
Search
Fetch Message
Get Quota
Logout
Delete Message
Mark Message UnRead 
Move Message
   
 

Update: Added support for

SSL Connection and verified with gmail
Copy Message
Move Message
Delete Message 
Mark Message Unread  