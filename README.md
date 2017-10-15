# Social Net
Simplest example of a social network infrastructure.
Just another one killer of Facebook or Google+ services.
## Target users
The service will support multiple user roles for different purposes. Currently, roles array looks alike:
1. Regular user. Can:
    1. Manipulate by own user identity;
        1. Register own identity;
        1. Specify some info about him or herself;
        1. Add userpic;
        1. Block own user identity;
    1. Exchange messages with another users:
        1. Post new message on the own wall;
        1. Post new message on another user walls;
        1. Remove own message from all user walls;
        1. View all messages from own wall;
        1. View messages from friend walls.
    1. Manage friends list:
        1. Add another user to user frieds list;
        1. Remove another user from user friends list; 
1. Moderator. In addition to a regular user oppotunities list can:
    1. Block another user identity;
    1. Remove another user messages from all user walls.
## Services
Our social network architecture will consider the next microservices list:
1. User identity management;
1. User account management;
    1. User session management;
1. User roles management;
1. Messages storage management + storage;
1. Frontend service;
1. Load balancer (not in scope of this project).
## Frameworks and Tools
The requirements for dev tools and frameworks should be as low as possible (and cheap):
* IDE: Visual Studio 2017 (free Community or Code edition should be enough);
* Main microservice framework: Microsoft ASP.NET Core 2.0;
* Microservice REST API discoverer: Swagger 2.0;
* Fronetnd framework: Angular 2, TypeScript;
# Useful links
* Google Site Reliability Engineering (<i>free ebook without registration and SMS</i>): https://landing.google.com/sre/
