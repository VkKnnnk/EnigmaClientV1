# EnigmaClient Version 1.22
> [!WARNING]
> Приложение работает с системными настройками ОС Windows, во избежании нежелательных изменений в системе,
>  рекомендуется запускать проект на ***виртуальной машине***.
## Описание
EnigmaClientV1.22 - [CRM-проект](https://github.com/VkKnnnk/EnigmaClientV1.2022?tab=readme-ov-file#аутентификация), который является десктопным приложением,
упрощающий функционирование бизнес-процессов в компьютерном клубе.
Он обеспечивает безопасность для инфраструктуры и удобство для клиента.

Задачей проекта являлось изучение предметной области 
компьютерного клуба, разработка приложения,
которое ограничивает доступ пользователя к системным настройкам ОС
и предоставляет удобный интерфейс для запуска приложений.
> Это курсовой проект, который стал для меня ценным опытом, позволяющим применить и расширить мои теоретические знания на практике. В нем я реализовывал свои идеи, решал сложные задачи, попутно изучая новые аспекты профессии.<p>
## Функционал
<p>Пользователь</p>
  
* Аутентификация и регистрация
* Пополнение лицевого счета и оплата услуг
* Изменение персональных данных
* Выход из аккаунта
* Запуск разрешенных приложений
<p>Приложение</p>
  
* Контроль времени использования компьютера
* Ограничение возможностей пользователя в ОС
* Отображение актульной информации по услугам
<p>Администратор</p>
  
* Отключение ограничений в ОС
* Генерация нового чека для регистрации
* Выход из приложения

## Инуструкция
> [!IMPORTANT]
> Приложение следует запускать ***от имени администратора***, чтобы программа имела доступ к функциям ОС.

### Аутентификация
<p align="left"><picture> <img src="ReadmeImg/auth.png" alt="Окно аутентификации" width="500"/></picture></p>
После запуска приложения открывается окно аутентификации,
где пользователь вводит свой логин и пароль.
После успешной аутентификации, открывается основное окно.</p>

Если вы используете приложение впервые, вам потребуется регистрация.
Для регистрации нового пользователя необходимо ввести уникальную комбинацию символов - чек,
который выдается новому пользователю.
> Выглядеть он может следующим образом:
> * 3333333333
> * 1231231231
> * 0015841235
<p align="left"><picture> <img src="ReadmeImg/reg.png" alt="Окно аутентификации" width="500"/></picture></p>
Зная свой уникальный чек, пользователь вводит его и попадает в окно регистрации.
Здесь заполняются формы "Имя", "Логин" и "Пароль", после чего, при нажатии "Зарегестрироваться"
в базе данных создается новый пользователь, который теперь может пройти аутентификацию.

 
