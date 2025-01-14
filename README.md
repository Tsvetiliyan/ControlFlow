# ControlFLow Application

ControlFlow е уеб приложение за управление на задачи, което позволява на потребителите да създават, управляват и проследяват задачите си. Приложението предлага възможности за задаване на приоритети на задачите, напомняния и потребителска автентикация. Task Manager е разработен с помощта на **ASP.NET Core** и е идеален за лична употреба или за малки екипи, които искат да организират своите задачи и проекти.

## Възможности

- **Потребителска автентикация** с ASP.NET Core Identity, което позволява на потребителите да се регистрират, влизат и управляват своите задачи.
- **Управление на задачи по приоритет** (висок, среден, нисък), с различни цветови индикатори за лесно разграничаване на важността.
- **Напомняния** за задачи, които могат да се изпращат по имейл към потребителя.
- **Абонаментни планове** с различни функционалности (безплатен и Pro план), като Pro планът предлага допълнителни функции за управление на задачите. (Тази функционалност ще бъде имплементирана в близкото бъдеще)
## Инсталация

### Изисквания
- Инсталиран **.NET 9** или по-нова версия.
- Настройка на база данни за **ControlFlow.Data**.
- Настройка на конфигурационни файлове за **ControlFlow.Services**.

За да стартираш приложението на твоя компютър, следвай тези стъпки:

### 1. Клонирай репозитория:

Използвай Git, за да клонираш репозиторията на твоя локален компютър:

```bash
git clone https://github.com/Tsvetiliyan/ControlFlow

```
## Документация на кода

# ControlFlow Solution

Това е решение, което демонстрира различни концепции и подходи при изграждането на приложение с многослойна архитектура. Решението съдържа пет основни проекта, всеки от които отговаря за различна част от функционалността на приложението.


## Архитектура на проекта

### Обща архитектура
Решението използва многослойна архитектура, която позволява ясното разделяне на отговорностите:
1. **ControlFlow** - Потребителски интерфейс и основна бизнес логика.
2. **ControlFlow.Core** - Основна обработка на данни и логика за управление на потока.
3. **ControlFlow.Data** - Достъп до данни и работа с бази данни.
4. **ControlFlow.Services** - Допълнителни услуги за конфигурация, логиране и управление на зависимости.
5. **ControlFlow.StaticDetails** - Статични данни и конфигурации, използвани в цялото приложение.

---

## ControlFlow
### Описание
Проектът **ControlFlow** е основното приложение, което осигурява взаимодействие с потребителя и обработка на логиката на контролния поток. Това е интерфейсът, с който потребителите взаимодействат, за да изпълняват основните операции на приложението.

### Ключови характеристики
- Потребителски интерфейс за работа с данни за контролния поток.
- Основна логика за обработка на входни данни от потребителите.
- Използва различни компоненти от другите проекти за управление на данни и услуги.

---

## ControlFlow.Core
### Описание
Проектът **ControlFlow.Core** съдържа основната бизнес логика и правилата за обработка на данни. Той предоставя абстракции за основни операции върху контролния поток, които могат да се използват от другите проекти.

### Ключови характеристики
- Основни класове и интерфейси за обработка на данни.
- Логика за изчисление на различни типове контролни потоци (например, цикли, условни изрази и др.).
- Абстракции за работа с данни, които могат да се използват в **ControlFlow.Data** и **ControlFlow.Services**.

### Обяснение на кода
- **ControlFlow.Core** съдържа абстракции и основни функции за работа с логиката на приложението, които могат да се използват в различни контексти.
- Чрез класове като `ControlFlowProcessor` и `FlowRulesManager`, проектът осигурява функционалността за обработка на различни алгоритми.
- Основни класове: `FlowProcessor`, `FlowRules`.

---

## ControlFlow.Data
### Описание
Проектът **ControlFlow.Data** отговаря за работата с данни, тяхното съхранение и извличане. Той съдържа контексти за бази данни и класове за достъп до данни.

### Ключови характеристики
- Управление на базата данни.
- Реализация на CRUD (Create, Read, Update, Delete) операции.
- Репозитории за достъп до данни и абстракции за взаимодействие с базата данни.

### Обяснение на кода
- **ControlFlow.Data** използва Entity Framework Core за комуникация с базата данни.
- Класовете в **ControlFlow.Data** създават абстракция за операциите с базата данни чрез `ControlFlowContext` и `FlowRepository`, които предоставят методи за извличане и съхранение на данни.
- Основни класове: `ControlFlowContext`, `ControlFlowRepository`.

---

## ControlFlow.Services
### Описание
Проектът **ControlFlow.Services** осигурява различни услуги, като управление на конфигурации, логиране и зависимостите на приложението. Този проект е отговорен за извършване на действия извън основната бизнес логика и работа с външни ресурси и API.

### Ключови характеристики
- Логиране на събития и обработка на изключения.
- Конфигурация на настройките на приложението.
- Осигуряване на услуги, които могат да се инжектират в други проекти.

### Обяснение на кода
- **ControlFlow.Services** използва **Dependency Injection** за регистриране на различни услуги като логиране, обработка на конфигурации и други помощни услуги.
- Проектът предоставя основни услуги като `LoggingService` и `ConfigurationService`, които са достъпни за други части на приложението.
- Основни класове: `LoggerService`, `AppSettings`.

---

## ControlFlow.StaticDetails
### Описание
Проектът **ControlFlow.StaticDetails** съдържа статични данни, които могат да се използват в различни части на приложението, като конфигурации, подробности за статуси или предварително дефинирани стойности, които не се променят по време на работа на приложението.

### Ключови характеристики
- Статични данни за различни статуси на потока (например, типове контроли или приоритети).
- Модели за статични детайли, които могат да бъдат използвани в другите проекти.
- Справочни данни, които не подлежат на промяна.

### Обяснение на кода
- **ControlFlow.StaticDetails** осигурява справочни данни като `FlowStatusDetails` или `PriorityLevels`, които се използват за конфигуриране и персонализиране на логиката на приложението.
- Тези данни се предоставят на други проекти като **ControlFlow.Core** и **ControlFlow.Services**, за да се улесни управлението на статични стойности и конфигурации.
- Основни класове: `FlowStatusDetails`, `PriorityLevels`.

---




