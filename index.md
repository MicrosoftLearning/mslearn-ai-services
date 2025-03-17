---
title: Azure AI Services Exercises
permalink: index.html
layout: home
---

# Azure AI Services Exercises

The following exercises are designed to support the modules on Microsoft Learn.


{% assign labs = site.pages | where_exp:"page", "page.url contains '/Instructions/Exercises'" %}
{% for activity in labs  %}
{% unless activity.url contains 'ai-foundry' %}
- [{{ activity.lab.title }}]({{ site.github.url }}{{ activity.url }})
{% endunless %}
{% endfor %}
