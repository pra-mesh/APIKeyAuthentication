# API Key authentication

## Introduction

One of the command way to secure a REST APIs are by using API keys. What is API key?
It is a special codes that are stored in a storage file like appsettings.json or environment variables that are used
to implement authentication to secure your end points.

Generally an API keys are passed through an header in a request pipeline there are different ways of securing your API keys:

1. Custom Attribute: It is used when you want to secure certain part of your code using an attributes like Authorize.
2. Custom Middleware: If you to check every request that comes through then we use custom middleware.
1. 