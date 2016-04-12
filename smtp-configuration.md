---
layout: page
title: Configuration SMTP for Formulate Emails
---

# Configuring SMTP for Emails
If you'd like to send emails when Formulate forms are submitted, you'll have to configure SMTP. There are many ways to do this, though they all involve modifying the web.config. If you just want to test emails locally, you can configure SMTP to deliver emails to your file system as `.eml` files. You can do that by making these changes to your web.config:

```
<configuration>
  <system.net>
    <mailSettings>
      <smtp deliveryMethod="SpecifiedPickupDirectory">
        <specifiedPickupDirectory pickupDirectoryLocation="c:\temp-email"/>
      </smtp>
    </mailSettings>
  </system.net>
</configuration>
```

As long as you ensure the `temp-email` folder exists, you should see files appear there whenever you submit a Formulate form (assuming you have set up the form handler properly).