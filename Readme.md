# Сбор данных о погодных условиях

## Оборудование
<ul>
<li> Датчик температуры и влажности Dht-22
<li>Raspbery py4 B
</ul>
список в дальнейшем будет пополняться

## Инструкция по запуску
<ul>
<li>Устанавливаем на расбери ОС <a href = "https://www.raspberrypi.com/software/">инструкция с офф сайта</a>
<li>устанавливаем язык программирования rust
<pre>
<code>
curl --proto '=https' --tlsv1.2 https://sh.rustup.rs -sSf -v -4 | sh

export PATH="$HOME/.cargo/bin:$PATH"
</code>
</pre>
<li> Клонируем репозиторий командой git clone
<li>Собираем проект командой cargo build и запускаем cargo run
<li> подключаем датчик как <a href = "https://habrastorage.org/storage2/a84/bd3/77a/a84bd377a9ad2d3bbe7376a0b89418d0.jpg">тут</a> 
</ul>

## Технологии на веб части
<ul>
<li> Asp.net core MVC
<li> SignalR
<li> Mass Transit
<li> EF Core
<ul>

## Поток данных
После получения данных с датчика они пишутся в очередь RabitMq

Если Rabit запущен на другом компьютере в той же сети(как у меня), то нужно применить следующую конфигурацию

<pre>
<code>
NODE_IP_ADDRESS=0.0.0.0
NODE_PORT=5672
</code>
</pre>
Далее данные считываются Consumer-ом

## Что нужно сделать
<ul>
<li> Добавление новых датчиков
<li> Добавить контейнеризацию
<li> Вынести Web на вторую распбери
<li> Рефакторинг backend части приложения
<li> Добавить выбор периода для просмотра данных
<li> Сконфигурировать логгирование при помощи serilog
</ul>