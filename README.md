# CryptoFollower

![Deploy status](https://github.com/mannaayd/CryptoFollower/actions/workflows/azure-functions-app-dotnet.yml/badge.svg) [![Unit tests](https://github.com/mannaayd/CryptoFollower/actions/workflows/unit-testing-app-dotnet.yml/badge.svg)](https://github.com/mannaayd/CryptoFollower/actions/workflows/unit-testing-app-dotnet.yml)

CryptoFollower is an Azure function designed to monitor real-time price changes for various cryptocurrencies. Its primary purpose is to notify users of any changes in prices via email or Telegram bot. 

The function runs every 30 seconds and triggers an alert if certain conditions are met. Once the alert is triggered, the function notifies user about price changes. Then it stores the current price of the cryptocurrency into Azure Table Storage.

The function has several configurable settings variables, including:
| Variable | Description |
|----------|-------------|
| CryptoCurrencyToWatch | the ID of the coin being monitored (e.g. "bitcoin") |
| IsAlertTriggerAbovePrice | indicates whether the alert should trigger when the price is above the target price or below it |
| ShouldBeNotifiedByTelegram | indicates whether the user should be notified by Telegram bot |
| TelegramApiKey | api key for your telegram bot |
| TelegramChatId | chat id with your telegram bot |
| AlertTriggerPrice | the target price for the alert |
| AlertCooldownMinutes | the cooldown time between alerts to prevent spamming |
| CheckCoinInformationSchedule | CRON expression that defines how often function will check price |
| AzureWebJobsStorage | the connection string for the Azure storage |
| APPLICATIONINSIGHTS_CONNECTION_STRING | the connection string for logging |

Note that these variables can be configured based on your specific needs and preferences. Be sure to adjust them accordingly before deploying the function.
