[![Build Status](https://travis-ci.org/mersocarlin/school-web-api.svg?branch=master)](https://travis-ci.org/mersocarlin/school-web-api)

# school-web-api
This is a sample for RESTful Web API where I build a simple back-end to manage students, teachers and courses.

## Also used in this project

* [x] Entity Framework (Code First)
* [x] Repository Pattern
* [x] Service Pattern
* [x] DDD
* [x] Dependency Injection
* [x] Unit Testing
* [x] Mock (Moq)
* [x] SQL Server
* [x] oAuth Authentication
* [ ] Cache for requests

## oAuth Token Generation

![oAuth Token Generation](https://github.com/mersocarlin/school-web-api/blob/master/images/token_request.PNG)

**grant_type**: *password*

**username**: *mersocarlin*

**password**: *BolshoiBooze*


## Sending requests

![Request with Token](https://github.com/mersocarlin/school-web-api/blob/master/images/request_with_token.PNG)

**Authorization**: Bearer [generated token]
