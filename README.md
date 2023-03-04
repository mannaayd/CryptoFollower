# CryptoFollower

![Deploy status](https://github.com/mannaayd/CryptoFollower/actions/workflows/azure-functions-app-dotnet.yml/badge.svg)

CryptoFollower is an Azure function designed to monitor real-time price changes for various cryptocurrencies. Its primary purpose is to notify users of any changes in prices via email or Telegram bot. 

The function runs every 30 seconds and triggers an alert if certain conditions are met. Once the alert is triggered, the function notifies USer about price changes. Then it stores the current price of the cryptocurrency into Azure Table Storage.

The function has several configurable settings variables, including:
| Variable | Description |
|----------|-------------|
| FollowedCryptoCurrency | the ID of the coin being monitored (e.g. "bitcoin") |
| TargetPriceCurrencyCode | the target price currency code (e.g. "usd") |
| IsAlertTriggerAbovePrice | indicates whether the alert should trigger when the price is above the target price or below it |
| IsNotifiedByMail | indicates whether the user should be notified by email |
| IsNotifiedByTelegram | indicates whether the user should be notified by Telegram bot |
| AlertTriggerPrice | the target price for the alert |
| AlertCooldownMinutes | the cooldown time between alerts to prevent spamming |
| AzureWebJobsStorage | the connection string for the Azure storage |
| APPLICATIONINSIGHTS_CONNECTION_STRING | the connection string for logging |

Note that these variables can be configured based on your specific needs and preferences. Be sure to adjust them accordingly before deploying the function.
