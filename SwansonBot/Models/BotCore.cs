using Microsoft.EntityFrameworkCore;
using Parcer.Models;
using Parcer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bots.Http;

namespace SwansonBot.Models
{
    public class BotCore
    {
        private ProductContext context;
        private IProductWriter productWriter;
        public BotCore(ProductContext productContext, IProductWriter writer )
        {
            context = productContext;
            productWriter= writer;
        }
        public async Task SendProductsFileAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            var file=Guid.NewGuid().ToString("D")+".xlsx";

            productWriter.SaveAs(file, await context.Products.ToListAsync());
            await using (Stream stream = System.IO.File.OpenRead(file)) 
            {
                await bot.SendDocumentAsync(
                    chatId: update.Message.Chat.Id,
                    document: InputFile.FromStream(stream: stream, fileName: "product.xlsx"),
                    caption: "List of products"
                    );
            }
            await Task.Run(()=>System.IO.File.Delete(file), cancellationToken);
        }
        public async Task FindProductAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            var code = update.Message.Text.Trim();

            var product = context.Products.FirstOrDefault(x => x.Code == code);
            if (product != null)
            {
                await bot.SendPhotoAsync(
                               chatId: update.Message.Chat.Id,
                               photo: InputFile.FromUri($"{product.ImgUrl}"),
                               caption:
                               $"<b>{(product.Available ? "🟢" : "🔴")}{product.Title}</b>\n" +
                               $"<b>{product.Description}</b>\n" +
                               $"${product.Price.ToString("F")}\n",
                               parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                               replyMarkup: new InlineKeyboardMarkup(
                            InlineKeyboardButton.WithUrl(
                            text: "Open on site",
                            url: product.FullUrl)));
            }
            else
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                        new KeyboardButton[] { "Download file" }
                    })
                {
                    ResizeKeyboard = true
                };
                //await bot.SendTextMessageAsync(update.Message.Chat, "Not found ❌");
                await bot.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Not found",
                    replyMarkup: replyKeyboardMarkup
                    );
            }
        }
        public async Task HandleUpdate(ITelegramBotClient bot,Update update, CancellationToken cancellationToken)
        {
            if (update.Message != null)
            {
                
                await bot.SendChatActionAsync(
                           chatId: update.Message.Chat.Id,
                           chatAction: Telegram.Bot.Types.Enums.ChatAction.Typing
                           );

                var text = update.Message.Text;
                if(text == "Download file" ) 
                {
                    await SendProductsFileAsync(bot, update, cancellationToken);
                }
                else
                {
                    await FindProductAsync(bot, update, cancellationToken);
                }
                return;
            }

            
           
        }
    }
}
