import Vue from "vue";
import VueI18n from "vue-i18n";

Vue.use(VueI18n);

const messages = {
  en: {
    translate_error: "Translation error",
    yes: "Yes",
    no: "No",
    close: "Close",
    App_nointernet:
      "No Internet connection! Some features may be unavailable at this time.",
    components_AppShell_AppBar_title: "IDD Timesheet Submission",
    components_AppShell_AppFooter_about: "About us",
    components_AppShell_AppFooter_opportunities: "Opportunities",
    components_AppShell_AppFooter_access: "Access",
    components_AppShell_AppFooter_phone:
      "Multnomah County General Information Line",
    components_AppShell_NavigationDrawer_home: "Home",
    components_AppShell_NavigationDrawer_upload: "Upload",
    components_AppShell_NavigationDrawer_about: "About",
    components_AppShell_NavigationDrawer_bug: "Report a bug",
    views_Home_upload: "Upload Timesheet",
    views_Home_about: "About",
    views_NotFound_text: "Page not found",
    views_About_learn: "Learn more",
    views_About_learn_desc:
      "To learn more about the Multnomah County Intellectual and Developmental Disabilites department, you can visit our website.",
    views_About_install: "How to Install",
    views_About_install_desc: "To install this app onto your mobile device:",
    views_About_install_li0: "Open the settings of the browser",
    views_About_install_li1: "Click 'Add to Home screen'",
    views_About_install_li2: "Navigate to the home screen of your phone",
    views_About_install_li3: "Open the 'IDD App'",
    views_About_install_li4: "Installation completed",
    views_About_contributors: "Contributors",
    views_About_contributors_desc:
      "Built in Collaboration with the Portland State University Capstone Program and Multnomah County.",
    views_About_contributors_position0: "Team Lead",
    views_About_contributors_position1: "Admin UI",
    views_About_contributors_position2: "Provider UI",
    views_About_contributors_position3: "Backend",
    views_Timesheet_continue: "Continue existing form?",
    views_Timesheet_continue_desc0:
      "Form already exists! You are working on form ",
    views_Timesheet_continue_desc1:
      "Do you want to continue or start a new form?",
    views_Timesheet_continue_btn0: "New form",
    views_Timesheet_continue_btn1: "Continue",
    views_Timesheet_select:
      "Select the type of form that you would like to submit",
    views_Timesheet_timesheet: "Timesheet",
    views_Timesheet_invalid:
      "Warning: We couldn’t read the text from the file you uploaded. You will have to manually enter all of the form fields.",
    views_Timesheet_select_form: "Please select a form type above.",
    views_Timesheet_upload_error: "FILE UPLOAD ERROR!",
    components_Forms_FileUploader_xPRS:
      "TIP: Uploading timesheets downloaded" +
      " from xPRS will be more accurate than photos from your camera." +
      " To learn how, click here.",
    components_Forms_FileUploader_dropfiles:
      "Drop files anywhere to upload<br/>or<br/>",
    components_Forms_FileUploader_selectfiles: "Select Files or Take a Picture",
    components_Forms_FileUploader_drop: "Drop files to upload",
    components_Forms_FileUploader_select: "Select Files",
    components_Forms_FileUploader_startupload: "Start Upload",
    components_Forms_FileUploader_stopupload: "Stop Upload",
    components_Forms_FileUploader_resetfiles: "Reset Files",
    components_Forms_FileUploader_success: "Success",
    components_Forms_FileUploader_processing: "Processing your files...",
    components_Forms_FileUploader_continue: "Continue...",
    components_Forms_FileUploader_offline:
      "OFFLINE: Can't upload file unless you are online.",
    components_Forms_ConfirmSubmission_submit: "Submit",
    components_Forms_ConfirmSubmission_cancel: "Cancel",
    components_Forms_ConfirmSubmission_confirm:
      "Are you sure want to submit the form?",
    components_Forms_ConfirmSubmission_edited:
      "There are edited fields. Please confirm these edits.",
    components_Forms_ConfirmSubmission_confirm_desc:
      "Please ensure your timesheet is correct and matches your time in" +
      " eXPRS. If it does not, it will be returned to you and may not be" +
      " processed in this pay period.",
    components_Forms_ConfirmSubmission_employer_desc:
      "Employer should confirm these edit(s).",
    components_Forms_ConfirmSubmission_provider_desc:
      "Provider should confirm these edit(s).",
    components_Forms_ConfirmSubmission_employer:
      "Employer accepts all changes to the IDD form.",
    components_Forms_ConfirmSubmission_provider:
      "Provider accepts all changes to the IDD form.",
    components_Forms_ConfirmSubmission_offline:
      "Offline! Please connect to the internet to submit.",
    components_Forms_ConfirmSubmission_submitting: "Submitting form...",
    components_Forms_ConfirmSubmission_error_desc: "Please try again.",
    components_Forms_ConfirmSubmission_error: "Something has gone wrong",
    components_Forms_ConfirmSubmission_home: "Return home",
    components_Forms_ConfirmSubmission_submitted_desc:
      "Thank you for submitting your timesheet. Please keep your" +
      " copy for your records. If there are any issues, IDD staff" +
      " will contact you via email.",
    components_Forms_ConfirmSubmission_submitted:
      "Your form has been submitted!",
    components_Forms_ConfirmSubmission_invalid: "Your form is not valid.",
    components_Forms_ConfirmSubmission_invalid_desc:
      "Please fix the invalid parts of the form and then retry submitting" +
      " your form.<hr />Errors:",
    components_Forms_FormField_edit: "Do you want to edit this field?",
    components_Forms_FormField_edit_desc:
      "This text was created based on the IDD timesheet that was uploaded." +
      " Sometimes, the app can't quite read your handwritting correctly, and" +
      " you will need to edit before sumbitting. This will ensure that your" +
      " timesheet is not returned as incorrect. Please make any corrections" +
      ' to match your timesheet exactly by selecting "Edit Field".',
    components_Forms_FormField_cancel: "Cancel edit",
    components_Forms_FormField_editbtn: "Edit field",
    components_Forms_ServicesDelivered_front: "Front side of the form",
    components_Forms_ServicesDelivered_totalhours: "Total Hours:",
    components_Forms_ServicesDelivered_back: "Back side of the form",
    components_Forms_ServicesDelivered_employer_verification:
      "<strong>PROVIDER/EMPLOYEE VERIFICATION:</strong><br />" +
      "<em>" +
      "I affirm that the data reported on this form is for actual dates/time I " +
      "worked delivering the service/supports listed to the recipient, that it " +
      "does not exceed the total amount of service authorized and was delivered " +
      "according to the recipient's service plan and provider/recipient service " +
      "agreement. I further acknowledge that reporting dates/tim worked in " +
      "excess of the amount of service authorized or not consistent with the " +
      "recipient's service plan may be considered Medicaid Fraud." +
      "</em>",
    components_Forms_ServicesDelivered_provider_verification:
      "<strong>RECIPIENT/EMPLOYER VERIFICATION:</strong><br />" +
      "<em>" +
      " I affirm that the data reported on this form is for actual dates/time" +
      " worked by the provider delivering the service/supports listed to the" +
      " recipient, that it does not exceed the total amount of service" +
      " authorized and was delivered according to the recipient's service plan" +
      " and provider/recipient service agreement." +
      "</em>",
    components_Forms_ServicesDelivered_authorize:
      "Providers submit this completed/signed form to the CDDP, Brokerage or CIIS " +
      "Program that authorized the service delivered.",
    components_Forms_ServicesDelivered_reset: "Reset form",
    components_Forms_Mileage_totalmiles: "Total Miles:",
    components_Forms_Mileage_delete:
      "Are you sure you want to delete this item?",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_title:
      "Service Delivered On:",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_save: "Save",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_group:
      "Group? (y/n)",
    components_Forms_ServicesDelivered_err0: "Invalid input",
    components_Forms_ServicesDelivered_err1:
      "ERROR: Invalid input in some form fields!",
    components_Forms_ServicesDelivered_err2: "Invalid time interval",
    components_Forms_ServicesDelivered_err3: "Invalid calculation",
    components_Forms_ServicesDelivered_err4: "Overlapping time interval",
    components_Forms_ServicesDelivered_err5_0: "ERROR: in row ",
    components_Forms_ServicesDelivered_err5_1: " of the timesheet table, ",
    components_Forms_ServicesDelivered_err5_2: " has the following errors: ",
    components_Forms_ServicesDelivered_err6:
      "ERROR: the employer or provider sign date is before the pay period.",
    components_Forms_ServicesDelivered_err7:
      "ERROR: the employer or provider sign date is before the latest service delivery date.",
    components_forms_servicesdelivered_row: "Row",

    ServicesDelivered_clientName_hint: "Full Name",
    ServicesDelivered_clientName_label: "Customer Name",
    ServicesDelivered_prime_hint: "Alphanumeric input",
    ServicesDelivered_prime_label: "Prime",
    ServicesDelivered_submissionDate_hint: "YYYY-MM",
    ServicesDelivered_submissionDate_label: "Pay Period Month and Year",
    ServicesDelivered_providerName_hint: "Full name",
    ServicesDelivered_providerName_label: "Provider Name",
    ServicesDelivered_providerNum_hint: "Six digit number",
    ServicesDelivered_providerNum_label: "Provider Number",
    ServicesDelivered_scpaName_hint: "Name of the SC PA",
    ServicesDelivered_scpaName_label: "SC/PA Name",
    ServicesDelivered_brokerage_hint: "Organization name",
    ServicesDelivered_brokerage_label: "CM Organization",
    ServicesDelivered_serviceAuthorized_hint: "Service provided",
    ServicesDelivered_serviceAuthorized_label: "Service",
    ServicesDelivered_totalHours_hint: "HH:mm",
    ServicesDelivered_totalHours_label: "Total Hours",
    ServicesDelivered_serviceGoal_hint: "Service Goal",
    ServicesDelivered_serviceGoal_label: "Service Goal",
    ServicesDelivered_progressNotes_hint: "Progress Notes",
    ServicesDelivered_progressNotes_label: "Progress Notes",
    ServicesDelivered_employerSignDate_hint: "YYYY-MM-DD",
    ServicesDelivered_employerSignDate_label: "Date",
    ServicesDelivered_providerSignDate_hint: "YYYY-MM-DD",
    ServicesDelivered_providerSignDate_label: "Date",
    ServicesDelivered_employerSignature_hint: "",
    ServicesDelivered_employerSignature_label:
      "Customer Employer or Employer Rep Signature",
    ServicesDelivered_providerSignature_hint: "",
    ServicesDelivered_providerSignature_label: "Provider/Employee Signature",
    ServicesDelivered_authorization_label:
      "I authorize the CDDP/Brokerage/CIIS staff to enter the data" +
      " reported on this form into eXPRS on my behalf for claims creation and payment.",
    ServicesDelivered_approval_label: "Provider Initials",
    Mileage_totalMiles_hint: "Total Miles",
    Mileage_totalMiles_label: "Total Miles",

    ServicesDeliveredTable_date_hint: "YYYY-MM-DD",
    ServicesDeliveredTable_date_label: "Date",
    ServicesDeliveredTable_starttime_hint: "HH:mm AM/PM",
    ServicesDeliveredTable_starttime_label: "Start/Time IN",
    ServicesDeliveredTable_endtime_hint: "HH:mm AM/PM",
    ServicesDeliveredTable_endtime_label: "End/Time OUT",
    MileageTable_purpose_hint: "Purpose",
    MileageTable_purpose_label: "Purpose",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_table_title:
      "Add/Edit Row",
    MileageTable_actions: "Actions",

    rules_required: "This field is required",
    rules_max0: "This field can't be over ",
    rules_max1: " characters",
    rules_alpha: "This field must be letters only",
    rules_alphanumeric: "This field must be letters or numbers",
    rules_numeric: "This field must be a number",
    rules_name: "Firstname Lastname",
    rules_timeOfDay: "This field must be in format HH:mm AM/PM",
    rules_time: "This field must be in format HH:mm",
    rules_monthYear: "This field must be in format YYYY-MM",
    rules_date: "This field must be in format YYYY-MM-DD",
    rules_email: "E-mail must be valid",
  },
  ru: {
    translate_error: "Ошибка с переводом",
    yes: "Да",
    no: "Нет",
    close: "Закрыть",
    App_nointernet:
      "Нет доступа к Интернету! Некоторые фичи могут быть отключёны в это время.",
    components_AppShell_AppBar_title: "IDD табели",
    components_AppShell_AppFooter_about: "О нас",
    components_AppShell_AppFooter_opportunities: "Возможности",
    components_AppShell_AppFooter_access: "Доступ",
    components_AppShell_AppFooter_phone:
      "Общая информационная линия округа Малтнома",
    components_AppShell_NavigationDrawer_home: "Главное",
    components_AppShell_NavigationDrawer_upload: "Пришлите табель",
    components_AppShell_NavigationDrawer_about: "Общая информация",
    components_AppShell_NavigationDrawer_bug: "Сообщите нам о баге",
    views_Home_upload: "Пришлите табель",
    views_Home_about: "Общая информация",
    views_NotFound_text: "Страница не найдена",
    views_About_learn: "Узнайте больше",
    views_About_learn_desc:
      "Чтобы узнать больше об IDD кафедре Малтнома, заходите на наш сайт.",
    views_About_install: "Как загрузить сайт",
    views_About_install_desc: "Чтобы загрузить наш сайт на ваш мобильник:",
    views_About_install_li0: "Откройте настройки браузера",
    views_About_install_li1: "Нажмите 'Add to Home screen'",
    views_About_install_li2: "Пойти на главный экран вашего мобильника",
    views_About_install_li3: "Откройте предложение 'IDD App'",
    views_About_install_li4: "Сайт загружён",
    views_About_contributors: "Участники",
    views_About_contributors_desc:
      "Разработано с помощью Capstone программы Портлендского Государственного Университета и Малтнома",
    views_About_contributors_position0: "Руководитель комманды",
    views_About_contributors_position1: "Интерфейс для администраторов",
    views_About_contributors_position2: "Интерфейс для провайдеров",
    views_About_contributors_position3: "Бэкенд",
    views_Timesheet_continue: "Продолжать работать над этом табелем?",
    views_Timesheet_continue_desc0: "Табель уже есть! Вы работаете над табелем",
    views_Timesheet_continue_desc1:
      "Хотите ли вы продольжать работать над этой табелем, или хотите пришлить новый табель?",
    views_Timesheet_continue_btn0: "Пришлите новый табель",
    views_Timesheet_continue_btn1: "Продолжайте работать над табелем",
    views_Timesheet_select: "Выбырайте тип табеля, который вы хотите пришлить",
    views_Timesheet_timesheet: "Табель",
    views_Timesheet_invalid:
      "Осторожно: Мы не успели читать табель, который вы пришли нам. Вам приходится вручную выпольнить весь табель.",
    views_Timesheet_select_form: "Выбырайте тип табеля, пожалуйста.",
    views_Timesheet_upload_error: "ОШИБКА: МЫ НЕ МОГЛИ ПРИШЛИТЬ ФАЙЛЫ!",
    components_Forms_FileUploader_dropfiles: "Тащите файлы сюда<br/>или<br/>",
    components_Forms_FileUploader_selectfiles:
      "Выбырайте файлы или Снимайте фотографию",
    components_Forms_FileUploader_drop: "Тащите файлы",
    components_Forms_FileUploader_select: "Выбырайте файлы",
    components_Forms_FileUploader_startupload: "Пришлите файлы",
    components_Forms_FileUploader_stopupload: "Остановите отправление файлы",
    components_Forms_FileUploader_resetfiles: "Удалить файлы",
    components_Forms_FileUploader_success: "Хорошо",
    components_Forms_FileUploader_processing: "Мы обрабатываем ваши файлы...",
    components_Forms_FileUploader_continue: "Продолжайте...",
    components_Forms_FileUploader_offline:
      "Нет доступа к Интернету: Невозможно пришлить ваши файлы без доступа к Интернету.",
    components_Forms_FileUploader_xPRS:
      "Внимание: Результат будет лучше, если вы сохраняете табель из eXPRS как .pdf. Чтобы узнать больше, нажмите здесь.",
    components_Forms_ConfirmSubmission_submit: "Пришлите табель",
    components_Forms_ConfirmSubmission_cancel: "Отменить",
    components_Forms_ConfirmSubmission_confirm: "Хотите ли вы пришлить табель?",
    components_Forms_ConfirmSubmission_edited:
      "Есть несколько изменений в табели. Утверждите данные изменения, пожалуйста.",
    components_Forms_ConfirmSubmission_confirm_desc:
      "Проверьте, что табель правильно отражает табель в eXPRS. Если нет, то" +
      " мы будим отправить табель обратно, и табель можеть не быть обработан в этом периоде.",
    components_Forms_ConfirmSubmission_employer_desc:
      "Работодатель должен утверждать данные изменения.",
    components_Forms_ConfirmSubmission_provider_desc:
      "Провайдер должен утверждать данные изменения.",
    components_Forms_ConfirmSubmission_employer:
      "Работодатель утверждает все изменеия IDD табеля.",
    components_Forms_ConfirmSubmission_provider:
      "Провайдер утверждает все изменеия IDD табеля.",
    components_Forms_ConfirmSubmission_offline:
      "Нет доступа к Интернету! Невозможно пришлать табель без доступа к Интернету.",
    components_Forms_ConfirmSubmission_submitting: "Присылаем табель...",
    components_Forms_ConfirmSubmission_error_desc:
      "Попробуйте ещё раз, пожалуйста...",
    components_Forms_ConfirmSubmission_error: "Произошла ошибка",
    components_Forms_ConfirmSubmission_home: "Вернутесь на главную страницу",
    components_Forms_ConfirmSubmission_submitted_desc:
      "Спасибо, мы пришли ваш табель. Сохраните копию этой страницы для себя." +
      " Если произойдёт ошибку, то представитель IDD кафедра Малтнома будет отправить вам имейл сообщение.",
    components_Forms_ConfirmSubmission_submitted: "Мы пришли ваш табель!",
    components_Forms_ConfirmSubmission_invalid: "В табели есть ошибки.",
    components_Forms_ConfirmSubmission_invalid_desc:
      "Исправьте ошибки в табели и попробуйте ещё раз, пожалуйста.<hr />Ошибки:",
    components_Forms_FormField_edit: "Хотите ли вы изменить эту информацию?",
    components_Forms_FormField_edit_desc:
      "Эта информация из IDD табеля, который вы пришлили." +
      " Иногдна мы не понимаем ваш почерк и вам нужно исправить информацию перед тем, как пришлить табель." +
      " Пожалуйста, проверьте, что информация правильна и исправтье ошибки.",
    components_Forms_FormField_cancel: "Отменить",
    components_Forms_FormField_editbtn: "Изменить информацию",
    components_Forms_ServicesDelivered_front: "Первая страница табеля",
    components_Forms_ServicesDelivered_totalhours: "Сумма часов:",
    components_Forms_ServicesDelivered_back: "Вторая страница табеля",
    components_Forms_ServicesDelivered_employer_verification:
      "<strong>Проверка провайдера:</strong><br />" +
      "<em>" +
      "Я утверждаю, что информация на этом табели правильно отражает время, когда я " +
      "оказался помощью работодатели, " +
      "и что время не высше разрешённой суммы времени для работодателя, в соотношении со соглашением плана работодателя. " +
      "Я также утверждаю, что отправление неправильнной информации или слишком много времени может считаться мошенничеством Medicaid." +
      "</em>",
    components_Forms_ServicesDelivered_provider_verification:
      "<strong>Проверка работодателя:</strong><br />" +
      "<em>" +
      "Я утверждаю, что информация на этом табели правильно отражает время, когда провайдер " +
      "оказался помощью работодатели, " +
      "и что время не высше разрешённой суммы времени для работодателя, в соотношении со соглашением плана работодателя. " +
      "</em>",
    components_Forms_ServicesDelivered_authorize:
      "Провайдеры присылают табель CDDP, брокеражу или CIIS " +
      "программе, которая разрешила выпольнение обслуживании.",
    components_Forms_ServicesDelivered_reset: "Убрать все изменения табеля",
    components_Forms_Mileage_totalmiles: "Сумма милов:",
    components_Forms_Mileage_delete: "Хотите ли вы удалить эту строку?",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_title:
      "Выпольнение обслуживания:",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_save: "Сохранить",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_group:
      "Группа? (да/нет)",
    components_Forms_ServicesDelivered_err0: "Информация неправильна",
    components_Forms_ServicesDelivered_err1: "ОШИБКА: Информация неправильна",
    components_Forms_ServicesDelivered_err2: "Неправильный промежуток времени!",
    components_Forms_ServicesDelivered_err3: "Вычисление суммы неправильно",
    components_Forms_ServicesDelivered_err4: "Промежутки времени совпадаются",
    components_Forms_ServicesDelivered_err5_0: "ОШИБКА: в строке ",
    components_Forms_ServicesDelivered_err5_1: " табеля, у ",
    components_Forms_ServicesDelivered_err5_2: " есть следующие ошибки: ",
    components_Forms_ServicesDelivered_err6:
      "ОШИБКА: день подписания для работодателя или провайдера перед периодом заработки.",
    components_Forms_ServicesDelivered_err7:
      "ОШИБКА: день подписания для работодателя или провайдера перед последным днём выпольнения обслуживании.",
    components_forms_servicesdelivered_row: "Строка",

    ServicesDelivered_clientName_label: "Имя работодателя",
    ServicesDelivered_clientName_hint: "Польное Имя",
    ServicesDelivered_prime_hint: "Цифры и буквы",
    ServicesDelivered_prime_label: "Номер Прайм",
    ServicesDelivered_submissionDate_hint: "ГГГГ-ММ",
    ServicesDelivered_submissionDate_label: "Месяц и год периода заплаты",
    ServicesDelivered_providerName_hint: "Польное Имя",
    ServicesDelivered_providerName_label: "Имя провайдера",
    ServicesDelivered_providerNum_hint: "Шесть цифр",
    ServicesDelivered_providerNum_label: "Номер провайдера",
    ServicesDelivered_scpaName_hint: "Имя SC PA",
    ServicesDelivered_scpaName_label: "Имя SC/PA",
    ServicesDelivered_brokerage_hint: "Название организации",
    ServicesDelivered_brokerage_label: "Организации CM",
    ServicesDelivered_serviceAuthorized_hint:
      "Обслуживание, которым вы оказывали",
    ServicesDelivered_serviceAuthorized_label: "Обслуживание",
    ServicesDelivered_totalHours_hint: "ЧЧ:мм",
    ServicesDelivered_totalHours_label: "Сумма часов",
    ServicesDelivered_serviceGoal_hint: "Какая была цель обслуживания",
    ServicesDelivered_serviceGoal_label: "Цель обслуживания",
    ServicesDelivered_progressNotes_hint: "Как идёт прогресс",
    ServicesDelivered_progressNotes_label: "Конспексты прогресса",
    ServicesDelivered_employerSignDate_hint: "ГГГГ-ММ-ДД",
    ServicesDelivered_employerSignDate_label: "День",
    ServicesDelivered_providerSignDate_hint: "ГГГГ-ММ-ДД",
    ServicesDelivered_providerSignDate_label: "День",
    ServicesDelivered_employerSignature_hint: "",
    ServicesDelivered_employerSignature_label:
      "Подписание работодателя или представитель работодателя",
    ServicesDelivered_providerSignature_hint: "",
    ServicesDelivered_providerSignature_label: "Подписание провайдера",
    ServicesDelivered_authorization_label:
      "Я разрешаю CDDP/Брокеража/CIIS пришлить информацию в этом" +
      " табели в eXPRS вместо меня для зарплаты провайдера",
    ServicesDelivered_approval_label: "Аббревиатура имени провайдера",
    Mileage_totalMiles_hint: "Сумма майлов",
    Mileage_totalMiles_label: "Сумма майлов",

    ServicesDeliveredTable_date_hint: "ГГГГ-ММ-ДД",
    ServicesDeliveredTable_date_label: "День",
    ServicesDeliveredTable_starttime_hint: "ЧЧ:мм AM/PM",
    ServicesDeliveredTable_starttime_label: "Начало/С",
    ServicesDeliveredTable_endtime_hint: "ЧЧ:мм AM/PM",
    ServicesDeliveredTable_endtime_label: "Конец/До",
    MileageTable_purpose_hint: "Причина",
    MileageTable_purpose_label: "Причина",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_table_title:
      "Добавить/Изменить строку",
    MileageTable_actions: "Действие",

    rules_required: "Эта информация обязательно нужна",
    rules_max0: "Это должно быть меньше ",
    rules_max1: " букв",
    rules_alpha: "Только буквы латинского алфавита",
    rules_alphanumeric: "Только буквы латинского алфавита или цифра",
    rules_numeric: "Только цифры",
    rules_name: "Имя Фамилия в латинском алфавите",
    rules_timeOfDay: "Только в формате ЧЧ:мм AM/PM",
    rules_time: "Только в формате ЧЧ:мм",
    rules_monthYear: "Только в формате ГГГГ-ММ",
    rules_date: "Только в формате ГГГГ-ММ-ДД",
    rules_email: "Имейл дольжен быть правилен",
  },
  es: {
    translate_error: "Error de traducción",
    yes: "Sí",
    no: "No",
    close: "Cerrar",
    App_nointernet:
      "¡Sin conexión a Internet! Es posible que algunas funciones no estén disponibles en este momento",
    components_AppShell_AppBar_title: "IDD Envío de parte de horas",
    components_AppShell_AppFooter_about: "Sobre nosotros",
    components_AppShell_AppFooter_opportunities: "Oportunidades",
    components_AppShell_AppFooter_access: "Acceso",
    components_AppShell_AppFooter_phone:
      "Línea de información general del condado de Multnomah",
    components_AppShell_NavigationDrawer_home: "Inicio",
    components_AppShell_NavigationDrawer_upload: "Cargar",
    components_AppShell_NavigationDrawer_about: "Sobre nosotros",
    components_AppShell_NavigationDrawer_bug: "Informar un error",
    views_Home_upload: "Subir hoja de horas",
    views_Home_about: "Sobre nosotros",
    views_NotFound_text: "Página no encontrada",
    views_About_learn: "Más información",
    views_About_learn_desc:
      "Para obtener más información sobre el departamento de discapacidades intelectuales y del desarrollo del condado de Multnomah, puede visitar nuestro sitio web",
    views_About_install: "Cómo instalar",
    views_About_install_desc:
      "Para instalar esta aplicación en tu dispositivo móvil:",
    views_About_install_li0: "Abrir la configuración del navegador",
    views_About_install_li1: "Haga clic en 'Add to Home screen'",
    views_About_install_li2: "Navega a la pantalla de inicio de tu teléfono",
    views_About_install_li3: "Abra 'IDD App'",
    views_About_install_li4: "Instalación completada",
    views_About_contributors: "Colaboradores",
    views_About_contributors_desc:
      "Construido en colaboración con el Programa Capstone de la Universidad Estatal de Portland y el Condado de Multnomah",
    views_About_contributors_position0: "Jefe",
    views_About_contributors_position1: "Admin UI",
    views_About_contributors_position2: "Proveedor UI",
    views_About_contributors_position3: "Backend",
    views_Timesheet_continue: "¿Continuar el formulario existente?",
    views_Timesheet_continue_desc0:
      "¡El formulario ya existe! Estás trabajando en el formulario ",
    views_Timesheet_continue_desc1:
      "¿Desea continuar o comenzar un nuevo formulario?",
    views_Timesheet_continue_btn0: "Nuevo formulario",
    views_Timesheet_continue_btn1: "Continuar",
    views_Timesheet_select: "Seleccione el tipo de formulario que desea enviar",
    views_Timesheet_timesheet: "Envío de parte de horas ",
    views_Timesheet_invalid:
      "Advertencia: no pudimos leer el texto del archivo que cargó. Tendrá que ingresar manualmente todos los campos del formulario.",
    views_Timesheet_select_form: "Seleccione un tipo de formulario arriba",
    views_Timesheet_upload_error: "ERROR DE CARGA DE ARCHIVO!",
    components_Forms_FileUploader_dropfiles:
      "Soltar archivos en cualquier lugar para cargar<br/>o<br/>",
    components_Forms_FileUploader_selectfiles:
      "Seleccionar archivos o tomar una foto",
    components_Forms_FileUploader_drop: "Soltar archivos para cargar",
    components_Forms_FileUploader_select: "Seleccionar archivos",
    components_Forms_FileUploader_startupload: "Iniciar carga",
    components_Forms_FileUploader_stopupload: "Detener carga",
    components_Forms_FileUploader_resetfiles: "Restablecer archivos",
    components_Forms_FileUploader_success: "Bueno",
    components_Forms_FileUploader_processing: "Procesando sus archivos...",
    components_Forms_FileUploader_continue: "Continuar...",
    components_Forms_FileUploader_offline:
      "DESCONECTADA: no se puede cargar el archivo a menos que esté en línea",
    components_Forms_FileUploader_xPRS:
      "SUGERENCIA: la carga de hojas de " +
      "tiempo descargadas de eXPRS será más precisa que las fotos de su cámara." +
      "Para aprender cómo, da click aquí.",
    components_Forms_ConfirmSubmission_submit: "Enviar",
    components_Forms_ConfirmSubmission_cancel: "Cancelar",
    components_Forms_ConfirmSubmission_confirm:
      "¿Está seguro de que desea enviar el formulario?",
    components_Forms_ConfirmSubmission_edited:
      "Hay campos editados. Confirme estas ediciones",
    components_Forms_ConfirmSubmission_confirm_desc:
      "Por favor, asegúrese de que su hoja de tiempo sea correcta y que mate su tiempo en" +
      " eXPRS. Si no lo hace, se le devolverá y es posible que no" +
      " procesado en este período de pago",
    components_Forms_ConfirmSubmission_employer_desc:
      "El empleador debe confirmar estas ediciones",
    components_Forms_ConfirmSubmission_provider_desc:
      "El proveedor debe confirmar estas ediciones",
    components_Forms_ConfirmSubmission_employer:
      "El empleador acepta todos los cambios en el formulario IDD",
    components_Forms_ConfirmSubmission_provider:
      "El proveedor acepta todos los cambios en el formulario IDD",
    components_Forms_ConfirmSubmission_offline:
      "Desconectada! Conéctese a Internet para enviar ",
    components_Forms_ConfirmSubmission_submitting: "Enviando formulario...",
    components_Forms_ConfirmSubmission_error_desc: "Vuelva a intentarlo",
    components_Forms_ConfirmSubmission_error: "Algo ha salido mal",
    components_Forms_ConfirmSubmission_home: "Inicio",
    components_Forms_ConfirmSubmission_submitted_desc:
      "Gracias por enviar su parte de horas. Por favor mantenga su" +
      " copia para sus registros. Si hay algún problema, el personal de IDD" +
      " se comunicará con usted por correo electrónico",
    components_Forms_ConfirmSubmission_submitted:
      "¡Su formulario ha sido enviado!",
    components_Forms_ConfirmSubmission_invalid: "Su formulario no es válido",
    components_Forms_ConfirmSubmission_invalid_desc:
      "Corrija las partes inválidas del formulario y luego vuelva a intentar enviar" +
      " su formulario. <hr /> Errores:",
    components_Forms_FormField_edit: "¿Desea editar este campo?",
    components_Forms_FormField_edit_desc:
      "Este texto se creó en base a la hoja de tiempo IDD que se cargó" +
      " A veces, la aplicación no puede leer tu letra correctamente, y" +
      " necesitará editar antes de sumbitar. Esto asegurará que su" +
      " hoja de horas no se devuelve como incorrecta. Por favor haga cualquier corrección" +
      ' para que coincida exactamente con su hoja de tiempo seleccionando "Editar campo".',
    components_Forms_FormField_cancel: "Cancelar edición",
    components_Forms_FormField_editbtn: "Editar campo",
    components_Forms_ServicesDelivered_front: "Anverso del formulario",
    components_Forms_ServicesDelivered_totalhours: "Horas en total",
    components_Forms_ServicesDelivered_back: "Parte posterior del formulario",
    components_Forms_ServicesDelivered_employer_verification:
      "<strong>VERIFICACIÓN DE PROVEEDOR / EMPLEADO:</strong><br />" +
      "<em>" +
      "Afirmo que los datos informados en este formulario son para fechas / horas reales que " +
      "trabajó entregando el servicio / soporte enumerado al destinatario, que " +
      "no excede la cantidad total de servicio autorizado y fue entregado " +
      "de acuerdo con el plan de servicio del destinatario y el servicio de proveedor / destinatario " +
      "acuerdo. Además, reconozco que las fechas / horas de informes funcionaron en " +
      "exceso de la cantidad de servicio autorizado o no compatible con el " +
      "plan de servicio del destinatario puede considerarse fraude a Medicaid." +
      "</em>",
    components_Forms_ServicesDelivered_provider_verification:
      "<strong>VERIFICACIÓN DE RECEPTOR / EMPLEADOR:</strong><br />" +
      "<em>" +
      " Afirmo que los datos informados en este formulario son para fechas / horas reales" +
      " trabajado por el proveedor que brinda el servicio / soporte enumerado en" +
      " destinatario, que no excede la cantidad total de servicio" +
      " autorizado y entregado de acuerdo con el plan de servicio del destinatario" +
      " y acuerdo de servicio proveedor / destinatario." +
      "</em>",
    components_Forms_ServicesDelivered_authorize:
      "Los proveedores envían este formulario completado / firmado al CDDP, Brokerage o CIIS " +
      "Programa que autorizó el servicio prestado",
    components_Forms_ServicesDelivered_reset: "Restablecer formulario",
    components_Forms_Mileage_totalmiles: "Horas en total:",
    components_Forms_Mileage_delete:
      "¿Está seguro de que desea eliminar este elemento?",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_title:
      "Servicio entregado:",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_save: "Guardar",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_group:
      "¿Grupo? (Sí/no)",
    components_Forms_ServicesDelivered_err0: "Entrada no válida",
    components_Forms_ServicesDelivered_err1:
      "ERROR: ¡Entrada no válida en algunos campos de formulario!",
    components_Forms_ServicesDelivered_err2: "Intervalo de tiempo no válido",
    components_Forms_ServicesDelivered_err3: "Cálculo no válido",
    components_Forms_ServicesDelivered_err4: "Intervalo de tiempo superpuesto",
    components_Forms_ServicesDelivered_err5_0: "ERROR: en fila ",
    components_Forms_ServicesDelivered_err5_1:
      " de la tabla del parte de horas, ",
    components_Forms_ServicesDelivered_err5_2:
      " tiene los siguientes errores: ",
    components_Forms_ServicesDelivered_err6:
      "ERROR: la fecha de firma del empleador o proveedor es anterior al período de pago",
    components_Forms_ServicesDelivered_err7:
      "ERROR: la fecha de firma del empleador o proveedor es anterior a la última fecha de entrega del servicio",
    components_forms_servicesdelivered_row: "Row",

    ServicesDelivered_clientName_hint: "Nombre completo",
    ServicesDelivered_clientName_label: "Nombre del cliente",
    ServicesDelivered_prime_hint: "Entrada alfanumérica",
    ServicesDelivered_prime_label: "Número Prime",
    ServicesDelivered_submissionDate_hint: "AAAA-MM",
    ServicesDelivered_submissionDate_label: "Período de pago Mes y año",
    ServicesDelivered_providerName_hint: "Numbre completo",
    ServicesDelivered_providerName_label: "Nombre del proveedor",
    ServicesDelivered_providerNum_hint: "Número de seis dígitos",
    ServicesDelivered_providerNum_label: "Número de proveedor",
    ServicesDelivered_scpaName_hint: "Nombre del SC PA",
    ServicesDelivered_scpaName_label: "Nombre del SC/PA ",
    ServicesDelivered_brokerage_hint: "Nombre de la organización",
    ServicesDelivered_brokerage_label: "CM Organización",
    ServicesDelivered_serviceAuthorized_hint: "Servicio proporcionado",
    ServicesDelivered_serviceAuthorized_label: "Servicio",
    ServicesDelivered_totalHours_hint: "HH:mm",
    ServicesDelivered_totalHours_label: "Horas en total",
    ServicesDelivered_serviceGoal_hint: "Objetivo del servicio",
    ServicesDelivered_serviceGoal_label: "Objetivo del servicio",
    ServicesDelivered_progressNotes_hint: "Notas",
    ServicesDelivered_progressNotes_label: "Notas de progreso",
    ServicesDelivered_employerSignDate_hint: "AAAA-MM-DD",
    ServicesDelivered_employerSignDate_label: "Fecha",
    ServicesDelivered_providerSignDate_hint: "AAAA-MM-DD",
    ServicesDelivered_providerSignDate_label: "Fecha",
    ServicesDelivered_employerSignature_hint: "",
    ServicesDelivered_employerSignature_label:
      "Firma del empleador del cliente o representante del empleador",
    ServicesDelivered_providerSignature_hint: "",
    ServicesDelivered_providerSignature_label: "Firma del proveedor / empleado",
    ServicesDelivered_authorization_label:
      "Autorizo al personal de CDDP / Brokerage / CIIS a ingresar los datos" +
      " informó en este formulario a eXPRS en mi nombre para la creación y el pago de reclamos",
    ServicesDelivered_approval_label: " Iniciales del proveedor",
    Mileage_totalMiles_hint: "Horas en total",
    Mileage_totalMiles_label: "Horas en total",

    ServicesDeliveredTable_date_hint: "AAAA-MM-DD",
    ServicesDeliveredTable_date_label: "Fecha",
    ServicesDeliveredTable_starttime_hint: "HH:mm AM/PM",
    ServicesDeliveredTable_starttime_label: "Comienzo/hora de inicio",
    ServicesDeliveredTable_endtime_hint: "HH:mm AM/PM",
    ServicesDeliveredTable_endtime_label: "Final/hora de finalización",
    MileageTable_purpose_hint: "Propósito",
    MileageTable_purpose_label: "Propósito",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_table_title:
      "Agregar / Editar fila",
    MileageTable_actions: "Acciones",

    rules_required: "Este campo es obligatorio",
    rules_max0: "Este campo no debe ser mayor que ",
    rules_max1: " caracteres",
    rules_alpha: "Este campo debe ser solo letras",
    rules_alphanumeric: "Este campo debe ser letras o números",
    rules_numeric: "Este campo debe ser un número",
    rules_name: "Nombre Apellido",
    rules_timeOfDay: "Este campo debe estar en formato HH:mm AM/PM",
    rules_time: "Este campo debe estar en formato HH:mm",
    rules_monthYear: "Este campo debe estar en formato AAAA-MM",
    rules_date: "Este campo debe estar en formato AAAA-MM-DD",
    rules_email: "El correo electrónico debe ser válido",
  },
  "zh-tw": {
    translate_error: "翻譯錯誤",
    yes: "是",
    no: "否",
    close: "關",
    App_nointernet: "網路沒連線! 有些功能無法使用.",
    components_AppShell_AppBar_title: "IDD 時間表申請",
    components_AppShell_AppFooter_about: "關於我們",
    components_AppShell_AppFooter_opportunities: "機會",
    components_AppShell_AppFooter_access: "Access",
    components_AppShell_AppFooter_phone:
      "Multnomah County General Information Line",
    components_AppShell_NavigationDrawer_home: "主頁",
    components_AppShell_NavigationDrawer_upload: "上傳",
    components_AppShell_NavigationDrawer_about: "關於",
    components_AppShell_NavigationDrawer_bug: "報告錯誤",
    views_Home_upload: "上傳時間表",
    views_Home_about: "關於",
    views_NotFound_text: "網頁未找到",
    views_About_learn: "學更多",
    views_About_learn_desc:
      "來學更多關於 Multnomah County Intellectual 和 Developmental Disabilites 部門, 請往我們的網站.",
    views_About_install: "怎麼安裝",
    views_About_install_desc: "安裝到隨身電腦:",
    views_About_install_li0: "打開網頁瀏覽器設定",
    views_About_install_li1: "點 '加入主畫面'",
    views_About_install_li2: "請往您的手機的主畫面",
    views_About_install_li3: "點 'IDD App'",
    views_About_install_li4: "安裝完成了",
    views_About_contributors: "貢獻著",
    views_About_contributors_desc:
      "Built in Collaboration with the Portland State University Capstone Program and Multnomah County.",
    views_About_contributors_position0: "小團領袖",
    views_About_contributors_position1: "管理用戶界面",
    views_About_contributors_position2: "服務提共者界面",
    views_About_contributors_position3: "後端",
    views_Timesheet_continue: "繼續已存在的表?",
    views_Timesheet_continue_desc0: "表已存在! 正在使用表 ",
    views_Timesheet_continue_desc1: "您想繼續或是開始新的表?",
    views_Timesheet_continue_btn0: "新表",
    views_Timesheet_continue_btn1: "繼續",
    views_Timesheet_select: "請選擇想要申請的表類",
    views_Timesheet_timesheet: "時間表",
    views_Timesheet_invalid:
      "警告: 我們無法讀成功您所上傳的檔案裡面的字. 您必須自己填寫表裡的每一個表格.",
    views_Timesheet_select_form: "請從上面選擇表類.",
    views_Timesheet_upload_error: "上傳錯誤!",
    components_Forms_FileUploader_dropfiles:
      "將文件托放到任何位直以上傳<br/>或<br/>",
    components_Forms_FileUploader_selectfiles: "選擇檔案或派照",
    components_Forms_FileUploader_drop: "托放檔案以上傳",
    components_Forms_FileUploader_select: "選擇檔案",
    components_Forms_FileUploader_startupload: "開始上傳",
    components_Forms_FileUploader_stopupload: "停止上傳",
    components_Forms_FileUploader_resetfiles: "從選檔案",
    components_Forms_FileUploader_success: "成功",
    components_Forms_FileUploader_processing: "處理檔案中...",
    components_Forms_FileUploader_continue: "繼續...",
    components_Forms_FileUploader_offline: "未連線: 除非上限無法上傳檔案.",
    components_Forms_FileUploader_xPRS:
      "TIP: Uploading timesheets downloaded" +
      " from xPRS will be more accurate than photos from your camera." +
      " To learn how, click here.",
    components_Forms_ConfirmSubmission_submit: "申請",
    components_Forms_ConfirmSubmission_cancel: "取消",
    components_Forms_ConfirmSubmission_confirm: "確定您想申請此表嗎?",
    components_Forms_ConfirmSubmission_edited: "有修正過的表格. 請確定此修正.",
    components_Forms_ConfirmSubmission_confirm_desc:
      "請您確定您的時間表沒有錯誤及於" +
      "eXPRS的時間是一模一樣. 若有差別您的申請表會被退回去並且在此發薪水崎無法處理",
    components_Forms_ConfirmSubmission_employer_desc: "雇主該確認表上的修正.",
    components_Forms_ConfirmSubmission_provider_desc:
      "服務提共者該確認表上的修正.",
    components_Forms_ConfirmSubmission_employer: "雇主接受IDD表上所有的修正.",
    components_Forms_ConfirmSubmission_provider:
      "服務提共者接受IDD表上所有的修正.",
    components_Forms_ConfirmSubmission_offline: "未連線! 請上線已申請.",
    components_Forms_ConfirmSubmission_submitting: "申請表...",
    components_Forms_ConfirmSubmission_error_desc: "請再式一次.",
    components_Forms_ConfirmSubmission_error: "發生錯誤",
    components_Forms_ConfirmSubmission_home: "往回主頁",
    components_Forms_ConfirmSubmission_submitted_desc:
      "謝謝您申請您的時間表. 請留自己原本的" +
      " 為自己紀錄. 若有任何問題IDD員工" +
      " 會於電字郵件跟您聯繫.",
    components_Forms_ConfirmSubmission_submitted: "表申請成功!",
    components_Forms_ConfirmSubmission_invalid: "您的表有錯誤無法使用.",
    components_Forms_ConfirmSubmission_invalid_desc:
      "請修正有錯誤的表格再試者申請" + ".<hr />錯誤:",
    components_Forms_FormField_edit: "您想修正此表格嗎?",
    components_Forms_FormField_edit_desc:
      "此字音由所上傳的IDD時間表而來的." +
      " 有時候此APP無法看懂您的手寫造成您" +
      "申請前親自必須修正來確實時間表不會因為錯誤而被退回來." +
      '請選"修正"來匹配您的申請表所寫的字.',
    components_Forms_FormField_cancel: "取消修正",
    components_Forms_FormField_editbtn: "修正表格",
    components_Forms_ServicesDelivered_front: "表面",
    components_Forms_ServicesDelivered_totalhours: "Total Hours:",
    components_Forms_ServicesDelivered_back: "背面",
    components_Forms_ServicesDelivered_employer_verification:
      "<strong>PROVIDER/EMPLOYEE VERIFICATION:</strong><br />" +
      "<em>" +
      "I affirm that the data reported on this form is for actual dates/time I " +
      "worked delivering the service/supports listed to the recipient, that it " +
      "does not exceed the total amount of service authorized and was delivered " +
      "according to the recipient's service plan and provider/recipient service " +
      "agreement. I further acknowledge that reporting dates/tim worked in " +
      "excess of the amount of service authorized or not consistent with the " +
      "recipient's service plan may be considered Medicaid Fraud." +
      "</em>",
    components_Forms_ServicesDelivered_provider_verification:
      "<strong>RECIPIENT/EMPLOYER VERIFICATION:</strong><br />" +
      "<em>" +
      " I affirm that the data reported on this form is for actual dates/time" +
      " worked by the provider delivering the service/supports listed to the" +
      " recipient, that it does not exceed the total amount of service" +
      " authorized and was delivered according to the recipient's service plan" +
      " and provider/recipient service agreement." +
      "</em>",
    components_Forms_ServicesDelivered_authorize:
      "Providers submit this completed/signed form to the CDDP, Brokerage or CIIS " +
      "Program that authorized the service delivered.",
    components_Forms_ServicesDelivered_reset: "還原表",
    components_Forms_Mileage_totalmiles: "Total Miles:",
    components_Forms_Mileage_delete: "確定您想散取此表格嗎?",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_title:
      "Service Delivered On:",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_save: "存",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_group:
      "Group? (y/n)",
    components_Forms_ServicesDelivered_err0: "無效的輸入",
    components_Forms_ServicesDelivered_err1: "錯誤: 些表格含錯誤的輸入!",
    components_Forms_ServicesDelivered_err2: "時段無效",
    components_Forms_ServicesDelivered_err3: "算法無效",
    components_Forms_ServicesDelivered_err4: "衝疊時段",
    components_Forms_ServicesDelivered_err5_0: "錯誤: 此行 ",
    components_Forms_ServicesDelivered_err5_1: "時間表格, ",
    components_Forms_ServicesDelivered_err5_2: "含下列的錯誤: ",
    components_Forms_ServicesDelivered_err6:
      "錯誤: 雇主或服務提共者簽名時間在此發薪水期前.",
    components_Forms_ServicesDelivered_err7:
      "錯誤: 雇主或服務提共者簽名時間在最後服務時間前.",
    components_forms_servicesdelivered_row: "行",

    ServicesDelivered_clientName_hint: "姓名",
    ServicesDelivered_clientName_label: "Customer Name",
    ServicesDelivered_prime_hint: "英文字母數字",
    ServicesDelivered_prime_label: "Prime",
    ServicesDelivered_submissionDate_hint: "YYYY-MM",
    ServicesDelivered_submissionDate_label: "Pay Period Month and Year",
    ServicesDelivered_providerName_hint: "姓名",
    ServicesDelivered_providerName_label: "Provider Name",
    ServicesDelivered_providerNum_hint: "六個數字",
    ServicesDelivered_providerNum_label: "Provider Number",
    ServicesDelivered_scpaName_hint: "SC/PA 名稱",
    ServicesDelivered_scpaName_label: "SC/PA Name",
    ServicesDelivered_brokerage_hint: "組織名稱",
    ServicesDelivered_brokerage_label: "CM Organization",
    ServicesDelivered_serviceAuthorized_hint: "所提共的服務",
    ServicesDelivered_serviceAuthorized_label: "Service",
    ServicesDelivered_totalHours_hint: "HH:mm",
    ServicesDelivered_totalHours_label: "Total Hours",
    ServicesDelivered_serviceGoal_hint: "服務目標",
    ServicesDelivered_serviceGoal_label: "Service Goal",
    ServicesDelivered_progressNotes_hint: "進步狀況",
    ServicesDelivered_progressNotes_label: "Progress Notes",
    ServicesDelivered_employerSignDate_hint: "YYYY-MM-DD",
    ServicesDelivered_employerSignDate_label: "Date",
    ServicesDelivered_providerSignDate_hint: "YYYY-MM-DD",
    ServicesDelivered_providerSignDate_label: "Date",
    ServicesDelivered_employerSignature_hint: "",
    ServicesDelivered_employerSignature_label:
      "Customer Employer or Employer Rep Signature",
    ServicesDelivered_providerSignature_hint: "",
    ServicesDelivered_providerSignature_label: "Provider/Employee Signature",
    ServicesDelivered_authorization_label:
      "I authorize the CDDP/Brokerage/CIIS staff to enter the data" +
      " reported on this form into eXPRS on my behalf for claims creation and payment.",
    ServicesDelivered_approval_label: "Provider Initials",
    Mileage_totalMiles_hint: "總英里數",
    Mileage_totalMiles_label: "Total Miles",

    ServicesDeliveredTable_date_hint: "YYYY-MM-DD",
    ServicesDeliveredTable_date_label: "Date",
    ServicesDeliveredTable_starttime_hint: "HH:mm AM/PM",
    ServicesDeliveredTable_starttime_label: "Start/Time IN",
    ServicesDeliveredTable_endtime_hint: "HH:mm AM/PM",
    ServicesDeliveredTable_endtime_label: "End/Time OUT",
    MileageTable_purpose_hint: "目的",
    MileageTable_purpose_label: "Purpose",
    components_Forms_ServicesDelivered_ServicesDeliveredTable_table_title:
      "增加/修正行",
    MileageTable_actions: "Actions",

    rules_required: "必填項目",
    rules_max0: "此表格不能超過",
    rules_max1: "字",
    rules_alpha: "此表格只能含英文字母",
    rules_alphanumeric: "此表格只能含英文字母數字",
    rules_numeric: "此表格只能含數字",
    rules_name: "大名 姓名",
    rules_timeOfDay: "此表格的格式必須為 HH:mm AM/PM",
    rules_time: "此表格的格式必須為 HH:mm",
    rules_monthYear: "此表格的格式必須為 YYYY-MM",
    rules_date: "此表格的格式必須為 YYYY-MM-DD",
    rules_email: "電字郵件地址必須有效",
  },
};

const i18n = new VueI18n({
  locale: "en", // set locale
  fallbackLocale: "es", // set fallback locale
  messages, // set locale messages
});

export default i18n;
