// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using Parcer.Models;
using Parcer.Services;
using SwansonBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bots.Http;
using Telegram.Bots.Requests;

Console.WriteLine("Hello, World!");


// http://t.me/MySwansonIfoBot

// 6271492189:AAG0D7BR17XNT1MgHfAJEyXP-pI5NlVpPL4

var token = "6271492189:AAG0D7BR17XNT1MgHfAJEyXP-pI5NlVpPL4";

var bot = new TelegramBotClient(token);


Console.WriteLine("Start"+ bot.GetMeAsync().Result.FirstName);


var cts=new CancellationTokenSource();
var core = new BotCore(new ProductContext(), new ExcelProdictWriter());

bot.StartReceiving(
    core.HandleUpdate,
    // handle update
    //async (bot, update, cancellationToken) =>
    //{
    //    await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(update));

    //    if (update.Message != null)
    //    {
    //        var product = new ProductContext().Products.FirstOrDefault(x => x.Code == update.Message.Text);
    //        if (product != null)
    //        {
    //            await bot.SendPhotoAsync(
    //                           chatId: update.Message.Chat.Id,
    //                           photo: InputFile.FromUri($"{product.ImgUrl}"),
    //                           caption:
    //                           $"<b>{(product.Available ? "🟢" : "🔴")}{product.Title}</b>\n" +
    //                           $"<b>{product.Description}</b>\n" +
    //                           $"${product.Price.ToString("F")}\n",
    //                           parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
    //                           replyMarkup: new InlineKeyboardMarkup(
    //                        InlineKeyboardButton.WithUrl(
    //                        text: "Open on site",
    //                        url: product.FullUrl)));
    //        }
    //    }

    //    //робоче
    //    /*
    //            if (update.Message != null)
    //            {
    //                //await bot.SendTextMessageAsync(update.Message.Chat, "Ку-ку");
    //                //await bot.SendPhotoAsync(
    //                //    chatId:update.Message.Chat.Id,
    //                //    photo:InputFile.FromUri("https://media.swansonvitamins.com/images/items/master/SW257.jpg"),
    //                //    caption: 
    //                //    "<b>Swanson Premium- Daily Essentials Multi with Iron</b>\n"+
    //                //    "$15.99\n"+
    //                //    "<a href=\"https://www.swansonvitamins.com/p/swanson-premium-daily-multi-vitamin-mineral-250-caps\">Open</a>\n ",
    //                //    parseMode:Telegram.Bot.Types.Enums.ParseMode.Html               
    //                //    );


    //                var dbProduct = new ProductContext().Products.FirstOrDefault(x => x.Code == update.Message.Text);

    //                await bot.SendChatActionAsync(
    //                        chatId: update.Message.Chat.Id,
    //                        chatAction: Telegram.Bot.Types.Enums.ChatAction.Typing);
    //                await Task.Delay(2000);

    //                if (dbProduct != null)
    //                {
    //                    await bot.SendTextMessageAsync(update.Message.Chat, "Ку-ку");
    //                    await bot.SendPhotoAsync(
    //                        chatId: update.Message.Chat.Id,
    //                        photo: InputFile.FromUri($"{dbProduct.ImgUrl}"),
    //                        caption:
    //                        $"<b>{dbProduct.Title}</b>\n" +
    //                        $"<b>{dbProduct.Description}</b>\n" +
    //                        $"${dbProduct.Price.ToString()}\n" +
    //                        $"<a href=\"{dbProduct.FullUrl}\">Open</a>\n ",
    //                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
    //                        );
    //                }
    //                else
    //                {
    //                    await bot.SendTextMessageAsync(update.Message.Chat, "Not found 😔");
    //                }
    //            }
    //    */
    //    //відправка файлів
    //    //await bot.SendChatActionAsync(
    //    //           chatId: update.Message.Chat.Id,
    //    //           chatAction: Telegram.Bot.Types.Enums.ChatAction.Typing);
    //    //await Task.Delay(2000);

    //    //var file = "D:\\ITStep\\Мережеве\\Parcer\\Parcer\\bin\\Debug\\net6.0\\test1.xlsx";

    //    //await using Stream stream = System.IO.File.OpenRead(file);
    //    //await bot.SendDocumentAsync(
    //    //    chatId: update.Message.Chat.Id,
    //    //    document: InputFile.FromStream(stream: stream, fileName: "products.xlsx"),
    //    //    caption: "List of products");

    //    //кнопки
    //    //ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
    //    //{
    //    //    new KeyboardButton[]{"Search","Get list"},
    //    //    new KeyboardButton[]{"1","2","3"}
    //    //});
    //    //await bot.SendTextMessageAsync(
    //    //    chatId: update.Message.Chat.Id,
    //    //    text:"<b>Hello</b>\n*world*\n",
    //    //    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
    //    //    );
    //    //if(update.CallbackQuery!= null)
    //    //{
    //    //    await bot.AnswerCallbackQueryAsync(
    //    //       callbackQueryId: update.CallbackQuery.Id
    //    //    );
    //    //    await bot.DeleteMessageAsync(
    //    //        chatId: update.Message.Chat.Id,
    //    //        messageId: update.CallbackQuery.Message.MessageId
    //    //        );
    //    //}
    //    //InlineKeyboardMarkup inlineKeyboard = new(new[]
    //    //{

    //    //})



    //},
    // handle error
    async (bot, exeption, cancellationToken) =>
    {
        await Console.Out.WriteLineAsync(exeption.Message);
    },
    // option
    new Telegram.Bot.Polling.ReceiverOptions { AllowedUpdates = {}},
    // cancelatiin token
    cts.Token
        
    );

Console.ReadLine();