C# XMPP server implementation that supports EVE-Online API verification

What Works:
-User registration via EVE API (in Band)
-Signing in with Psi and Trillian
-Single User chatting

What is Questionably Working:
-Administration panel
-adding corp and alliances
-user display
-Load handling (is it going to melt under 200 users?)
-Resource Caching
-Users/Corps/Alliances
-Responses

What Doesn’t Work
-Signing in with multiple resources
-Groupchat
-Proper error handling for boundary cases


TODO:
-handling multiple connections from single user
-finish user admin page
-finish corp and alliance admin page
-write testbed to spam server with requests
-implement group chat
-Logging
