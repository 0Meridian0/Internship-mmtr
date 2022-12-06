# Internship-mmtr
Приложение по сокращению ссылок из интернета. Главное условие: ссылка находится в промежутке от 7 до 10 символов и не учитывает домены.
Так как мне нужно будет для проверки генерировать много ссылок, у меня должна быть возможность не просто через UI сгенерировать короткую ссылку, но и через обычный HTTP запрос должна быть возможность.

## Пример запроса: 
>https://localhost:7143/?link=https://professorweb.ru/my/ASP_NET/webforms_4_5/level2/2_2.php
>https://localhost:7143/1CWy2WvT8

## Ответ
//Token: 1CWy2WvT8
//LongUrl: https://professorweb.ru/my/ASP_NET/webforms_4_5/level2/2_2.php

