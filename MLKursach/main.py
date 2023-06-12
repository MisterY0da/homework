import telebot

import pandas as pd
from sklearn.pipeline import Pipeline
from sklearn.preprocessing import StandardScaler
from sklearn.linear_model import LinearRegression
from sklearn.impute import SimpleImputer

data = pd.read_csv('data.csv')

pipeline = Pipeline([
    ('imputer', SimpleImputer(strategy='median')),
    ('std_scaller', StandardScaler())
])

dataTransformed = data.drop(['AQI Value', 'Country', 'City', 'AQI Category', 'CO AQI Category', 'Ozone AQI Category', 'NO2 AQI Category', 'PM2.5 AQI Category', 'lat', 'lng'], axis = 1)
outcome = data['AQI Value']

data_prepared = pipeline.fit_transform(dataTransformed)

from sklearn.model_selection import train_test_split
data_train, data_test, outcome_train, outcome_test = train_test_split(data_prepared, outcome, test_size = 0.20)

some_data = dataTransformed.iloc[:5]
some_output = outcome.iloc[:5]
data_prepared_some = pipeline.transform(some_data)

linearRegression = LinearRegression().fit(data_prepared,outcome)
some_data = dataTransformed.iloc[:5]
some_output = outcome.iloc[:5]
data_prepared_some = pipeline.transform(some_data)

def check_predict(co, ozone, no2, pm25):
    return linearRegression.predict(pipeline.transform([[co, ozone, no2, pm25]]))


bot = telebot.TeleBot ('5792139971:AAE3qze7RYLWap6trcY0MKqJvRs0joqkSkU')


@bot.message_handler(commands=['start'])
def welcome(message):
    bot.send_message(message.chat.id, 'Привет! Назови CO, Ozone, '
                                      'NO2, PM2.5 через запятую.')

@bot.message_handler(content_types=['text'])
def answer(message):
    bot.send_message(message.chat.id, 'Анализируем...')
    user_data = message.text.split(',')

    answer = check_predict(user_data[0], user_data[1], user_data[2], user_data[3])

    if int(answer) <= 50:
        bot.send_message(message.chat.id, f'Значение качества воздуха: {answer}. Диапазон показывает, что качество воздуха хорошее и не представляет угрозы для здоровья.')
    elif int(answer) >= 51 and int(answer) <= 100:
        bot.send_message(message.chat.id, f'Значение качества воздуха: {answer}. Средний диапазон и приемлемое качество. Некоторые люди могут испытывать дискомфорт.')
    elif int(answer) >= 101 and int(answer) <= 150:
        bot.send_message(message.chat.id, f'Значение качества воздуха: {answer}. Качество воздуха в этом диапазоне вредно для чувствительных групп. Они испытывают дискомфорт при дыхании.')
    elif int(answer) >= 151 and int(answer) <= 200:
        bot.send_message(message.chat.id, f'Значение качества воздуха: {answer}. Диапазон указывает на нездоровое качество воздуха, и люди начинают испытывать такие эффекты, как затрудненное дыхание.')
    elif int(answer) >= 201 and int(answer) <= 300:
        bot.send_message(message.chat.id, f'Значение качества воздуха: {answer}. Качество воздуха в этом диапазоне очень нездоровое, и в случае чрезвычайных ситуаций могут быть выданы предупреждения о вреде для здоровья. Все люди, вероятно, будут затронуты.')
    elif int(answer) >= 301 and int(answer) <= 500:
        bot.send_message(message.chat.id, f'Значение качества воздуха: {answer}. Это опасная категория качества воздуха, и все могут испытывать серьезные последствия для здоровья, такие как дискомфорт при дыхании, удушье, раздражение дыхательных путей и т. д.')

    do_again(message)


def do_again(message):
    bot.send_message(message.chat.id, 'Проверить еще? Если да, то опять введи CO, Ozone, '
                                      'NO2, PM2.5 через запятую.')

bot.polling(none_stop=True, interval=0)

