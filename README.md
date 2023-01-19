## stereo8 website with micoservice architecture

after designing stereo8 website with asp.net core web application i became familiar with clean architecture and tried to redesign that project with this approach.

in this project i followed DDD design pattern and in core domain layer i defined application entites.in core application layer i define bahaviors of entites and designed DTO classes,mappers and also use CQRS pattern to divid read and write model.
all implementations like data access are in infrastructure layer.in this layer we have identity server 4 that provides access tokens to protect our apis.also i've implemented a rabbit mq message broker to communicate message through apis.

in UI layer i designed two web applications for frontend and backend of website.this apps will call web apis to send and recieve informations.
