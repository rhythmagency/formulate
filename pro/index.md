---
layout: page
title: Formulate Pro
---

# Formulate Pro

Formulate Pro is the paid package that adds additional functionality to Formulate.

Currently, this includes a form submission handler that facilitates sending designed emails using Razor view files.

# Installing Formulate Pro

You can install Formulate Pro using NuGet (search for "Formulate Pro").

# Paying for Formulate Pro

Formulate Pro isn't free. You can pay for it below. Licenses are forever (i.e., they include upgrades to new versions of Formulate Pro).

<div class="sandbox-notice">
  <h1>Sandbox Mode</h1>
  <p>This is currently a sandbox. These payments will not work.</p>
</div>
<div class="payment-option">
  <h2>Formulate Pro: One Project License ($20)</h2>
  <p>Click the button below to purchase a license for a single project.</p>
  <div id="paypal-button-container-20"></div>
</div>
<div class="payment-option">
  <h2>Formulate Pro: Individual Developer License ($50)</h2>
  <p>Click the button below to purchase a license for an individual developer.</p>
  <div id="paypal-button-container-50"></div>
</div>
<div class="payment-option">
  <h2>Formulate Pro: Agency License ($200)</h2>
  <p>Click the button below to purchase a license for all projects worked on by a single agency.</p>
  <div id="paypal-button-container-200"></div>
</div>
<script src="https://www.paypal.com/sdk/js?client-id=AfrnUk_KuwCXRegkqT2NJacEwMQ5D5tYdmwLntcuc-RtLDL7jfKwpvcVQ5PoYPkhQXN44Ch-5fSc7NMs&currency=USD"></script>
<script>
  setupButton('#paypal-button-container-20', '20.00');
  setupButton('#paypal-button-container-50', '50.00');
  setupButton('#paypal-button-container-200', '200.00');
  function setupButton(selector, amount) {
    paypal.Buttons({
      fundingSource: paypal.FUNDING.CARD,
      createOrder: function(data, actions) {
        return actions.order.create({
          purchase_units: [{
            amount: {
              currency_code: 'USD',
              value: amount
            }
          }]
        });
      },
      onApprove: function(data, actions) {
        return actions.order.capture().then(function(details) {
          alert('Your payment is complete. You are now licensed to use Formulate Pro.');
          let paypalContainer = document.querySelector(selector);
          paypalContainer.innerHTML = '<p class="confirmation-message">Your payment is complete. You are now licensed to use Formulate Pro.</p>'
        });
      }
    }).render(selector);
  }
</script>