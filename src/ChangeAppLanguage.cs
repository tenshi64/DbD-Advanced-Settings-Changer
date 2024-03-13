using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbD_Settings_Changer
{
    public partial class Main : Form
    {
        public void changeAppLanguage(string Language)
        {
            if (Language == "English")
            {
                errors["confs-dont-exist"] = "Dead by Daylight confiuration files do not exist! Please, launch the game.";
                errors["new-version"] = "A new version of the program has been found.\n\nDo you want to download it?";
                errors["new-version-fail"] = "Checking newer program versions failed!";
                errors["max-fps"] = "The maximum frame rate in Dead by Daylight is 120. The game will be displayed at 120 frames per second anyway. Program will set you a limit of ";
                errors["max-fps-continuation"] = " FPS anyway.";
                errors["delete-confs"] = "Are you sure you want to delete the configuration file and create a new one?";
                errors["delete-confs-title"] = "Delete backup config";
                errors["discord-link-fail"] = "There was a problem opening the Discord link!";
                errors["new-version-title"] = "Check for updates";
                errors["information"] = "Information";
                errors["error"] = "Error";
                errors["fps-error"] = "The creators of Dead by Daylight have blocked the ability to play at frame rates other than: 30, 59, 60, 90, 120\n\nIf it is possible to play at a different frame rate, join the Discord server and report it to us and we will remove this message.";

                errors["tip1"] = "Reset settings to those saved in the backup.";
                errors["tip2"] = "Reset FPS cap.";
                errors["tip3"] = "Set your own FPS cap.";
                errors["tip4"] = "Set your own game resolution.";
                errors["tip5"] = "Reset to system resolution.";
                errors["tip6"] = "Delete the backup configuration file and create a new one.";
                errors["tip7"] = "Official Discord Server.";


                cbCopyright.Text = "Allow copyrighted music";
                cbSurvivorToggle.Text = "Survivor Toggle Interactions Mode";
                cbKillerToggle.Text = "Killer Toggle Interactions Mode";
                lblInAppLang.Text = "In-app language:";
                lblChangeSettings.Text = "Game version:";
                menuAudio.Text = "Audio";
                menuGraphics.Text = "Graphics";
                menuFPS.Text = "FPS Cap";
                menuPresets.Text = "Graphic presets";
                menuSensitivity.Text = "Mouse sensitivity";
                menuGame.Text = "Game";
                lblWidth.Text = "Width";
                lblHeight.Text = "Height";
                label35.Text = "2D Resolution";
                btnResSet.Text = "Set";
                btnResReset.Text = "Reset";
                label1.Text = "3D Resolution";
                label3.Text = "View Distance Quality";
                lblAaQuality.Text = "Anti-Aliasing Quality";
                label5.Text = "Shadow Quality";
                label6.Text = "Post-Processing Quality";
                label7.Text = "Textures Quality";
                label8.Text = "Effects Quality";
                label9.Text = "Foliage Quality";
                label10.Text = "Shading Quality";
                label11.Text = "Animations Quality";

                btnVwLow.Text = "Low";
                btnVwMedium.Text = "Medium";
                btnVwHigh.Text = "High";
                btnVwUltra.Text = "Ultra";
                btnVwEpic.Text = "Epic";
                btnVwAwesome.Text = "Awesome";

                btnAaLow.Text = "Low";
                btnAaMedium.Text = "Medium";
                btnAaHigh.Text = "High";
                btnAaUltra.Text = "Ultra";
                btnAaEpic.Text = "Epic";
                btnAaAwesome.Text = "Awesome";

                btnShadLow.Text = "Low";
                btnShadMedium.Text = "Medium";
                btnShadHigh.Text = "High";
                btnShadUltra.Text = "Ultra";
                btnShadEpic.Text = "Epic";
                btnShadAwesome.Text = "Awesome";

                btnPpLow.Text = "Low";
                btnPpMedium.Text = "Medium";
                btnPpHigh.Text = "High";
                btnPpUltra.Text = "Ultra";
                btnPpEpic.Text = "Epic";
                btnPpAwesome.Text = "Awesome";

                btnTxtLow.Text = "Low";
                btnTxtMedium.Text = "Medium";
                btnTxtHigh.Text = "High";
                btnTxtUltra.Text = "Ultra";
                btnTxtEpic.Text = "Epic";
                btnTxtAwesome.Text = "Awesome";

                btnEffLow.Text = "Low";
                btnEffMedium.Text = "Medium";
                btnEffHigh.Text = "High";
                btnEffUltra.Text = "Ultra";
                btnEffEpic.Text = "Epic";
                btnEffAwesome.Text = "Awesome";

                btnFolLow.Text = "Low";
                btnFolMedium.Text = "Medium";
                btnFolHigh.Text = "High";
                btnFolUltra.Text = "Ultra";
                btnFolEpic.Text = "Epic";
                btnFolAwesome.Text = "Awesome";

                btnShLow.Text = "Low";
                btnShMedium.Text = "Medium";
                btnShHigh.Text = "High";
                btnShUltra.Text = "Ultra";
                btnShEpic.Text = "Epic";
                btnShAwesome.Text = "Awesome";

                btnAnimLow.Text = "Low";
                btnAnimMedium.Text = "Medium";
                btnAnimHigh.Text = "High";
                btnAnimUltra.Text = "Ultra";
                btnAnimEpic.Text = "Epic";
                btnAnimAwesome.Text = "Awesome";
                btnAaDisable.Text = "Disable";
                cbVsync.Text = "V-Sync";
                btnSetFPS.Text = "Set";
                btnResetFPS.Text = "Reset";
                label18.Text = "Set your own FPS cap";
                label22.Text = "Main Volume";
                label13.Text = "Menu Music";
                label21.Text = "Audio Quality";
                cbHeadphones.Text = "Headphones";

                btnAudioLow.Text = "Low";
                btnAudioMedium.Text = "Medium";
                btnAudioHigh.Text = "High";
                btnAudioUltra.Text = "Ultra";
                btnAudioEpic.Text = "Epic";
                btnAudioAwesome.Text = "Awesome";

                label17.Text = "Choose one of the graphic presets.";
                btnPresetLow.Text = "Super low";
                btnPresetBalanced.Text = "Balanced";
                btnPresetEpic.Text = "Awesome";

                label24.Text = "Killer (mouse)";
                label25.Text = "Killer (controller)";
                label29.Text = "Survivor (mouse)";
                label27.Text = "Survivor (controller)";
                labelFOV.Text = "Field of view (FOV)";

                lblGameLang.Text = "In-game language:";
                cbLegacyPrestige.Text = "Legacy Prestige Portraits";
                cbSprintToCancel.Text = "Sprint To Cancel";
                cbHapticsVibration.Text = "Haptics Vibration (PS5)";
                cbMuteFocusLost.Text = "Mute on focus lost";
                cbHeartIcon.Text = "Terror Radius Visual Feedback";
            }
            if (Language == "Polski")
            {
                errors["confs-dont-exist"] = "Pliki konfiguracyjne Dead by Daylight nie istnieją! Uruchom grę.";
                errors["new-version"] = "Znaleziono nową wersję programu.\n\nCzy chcesz ją pobrać?";
                errors["new-version-fail"] = "Sprawdzanie nowszych wersji programu nie powiodło się!";
                errors["max-fps"] = "Maksymalna liczba klatek na sekundę w Dead by Daylight to 120. Gra i tak będzie wyświetlana z szybkością 120 klatek na sekundę. Program ustawi ci limit na ";
                errors["max-fps-continuation"] = " FPS mimo to.";
                errors["delete-confs"] = "Czy na pewno chcesz usunąć plik konfiguracyjny i utworzyć nowy?";
                errors["delete-confs-title"] = "Usuń konfigurację kopii zapasowej";
                errors["discord-link-fail"] = "Wystąpił problem podczas otwierania linku Discord!";
                errors["new-version-title"] = "Sprawdź aktualizacje";
                errors["information"] = "Informacje";
                errors["error"] = "Błąd";
                errors["fps-error"] = "Twórcy Dead by Daylight zablokowali możliwość gry z inną liczbą klatek na sekundę niż: 30, 59, 60, 90, 120\n\nJeśli możliwa jest gra z inną liczbą klatek na sekundę, dołącz do serwera Discord i zgłoś to do nas, a my usuniemy tę wiadomość.";

                errors["tip1"] = "Zresetuj ustawienia do tych zapisanych w kopii zapasowej.";
                errors["tip2"] = "Zresetuj limit FPS.";
                errors["tip3"] = "Ustaw własny limit FPS.";
                errors["tip4"] = "Ustaw własną rozdzielczość gry.";
                errors["tip5"] = "Zresetuj do rozdzielczości systemu.";
                errors["tip6"] = "Usuń plik konfiguracyjny kopii zapasowej i utwórz nowy.";
                errors["tip7"] = "Oficjalny serwer Discord.";

                cbCopyright.Text = "Zezwalaj na muzykę chronioną prawem autorskim";
                cbSurvivorToggle.Text = "Tryb przełączania interakcji ocalałego";
                cbKillerToggle.Text = "Tryb przełączania interakcji zabójcy";
                lblInAppLang.Text = "Język w aplikacji:";
                lblChangeSettings.Text = "Wersja gry:";
                menuAudio.Text = "Dźwięk";
                menuGraphics.Text = "Grafika";
                menuFPS.Text = "Limit FPS";
                menuPresets.Text = "Graficz. presety";
                menuSensitivity.Text = "Czułość myszy";
                menuGame.Text = "Gra";
                lblWidth.Text = "Szerokość";
                lblHeight.Text = "Wysokość";
                label35.Text = "Rozdzielczość 2D";
                btnResSet.Text = "Ustaw";
                btnResReset.Text = "Zresetuj";
                label1.Text = "Rozdzielczość 3D";
                label3.Text = "Jakość odgległości renderowania";
                lblAaQuality.Text = "Jakość Anty-Aliasingu";
                label5.Text = "Jakość cieni";
                label6.Text = "Jakość Post-Processingu";
                label7.Text = "Jakość tekstur";
                label8.Text = "Jakość efektów";
                label9.Text = "Jakość trawy/natury";
                label10.Text = "Jakość cieniowania";
                label11.Text = "Jakość animacji";

                btnVwLow.Text = "Niska";
                btnVwMedium.Text = "Średnia";
                btnVwHigh.Text = "Wysoka";
                btnVwUltra.Text = "Ultra";
                btnVwEpic.Text = "Epicka";
                btnVwAwesome.Text = "Świetna";

                btnAaLow.Text = "Niska";
                btnAaMedium.Text = "Średnia";
                btnAaHigh.Text = "Wysoka";
                btnAaUltra.Text = "Ultra";
                btnAaEpic.Text = "Epicka";
                btnAaAwesome.Text = "Świetna";

                btnShadLow.Text = "Niska";
                btnShadMedium.Text = "Średnia";
                btnShadHigh.Text = "Wysoka";
                btnShadUltra.Text = "Ultra";
                btnShadEpic.Text = "Epicka";
                btnShadAwesome.Text = "Świetna";

                btnPpLow.Text = "Niska";
                btnPpMedium.Text = "Średnia";
                btnPpHigh.Text = "Wysoka";
                btnPpUltra.Text = "Ultra";
                btnPpEpic.Text = "Epicka";
                btnPpAwesome.Text = "Świetna";

                btnTxtLow.Text = "Niska";
                btnTxtMedium.Text = "Średnia";
                btnTxtHigh.Text = "Wysoka";
                btnTxtUltra.Text = "Ultra";
                btnTxtEpic.Text = "Epicka";
                btnTxtAwesome.Text = "Świetna";

                btnEffLow.Text = "Niska";
                btnEffMedium.Text = "Średnia";
                btnEffHigh.Text = "Wysoka";
                btnEffUltra.Text = "Ultra";
                btnEffEpic.Text = "Epicka";
                btnEffAwesome.Text = "Świetna";

                btnFolLow.Text = "Niska";
                btnFolMedium.Text = "Średnia";
                btnFolHigh.Text = "Wysoka";
                btnFolUltra.Text = "Ultra";
                btnFolEpic.Text = "Epicka";
                btnFolAwesome.Text = "Świetna";

                btnShLow.Text = "Niska";
                btnShMedium.Text = "Średnia";
                btnShHigh.Text = "Wysoka";
                btnShUltra.Text = "Ultra";
                btnShEpic.Text = "Epicka";
                btnShAwesome.Text = "Świetna";

                btnAnimLow.Text = "Niska";
                btnAnimMedium.Text = "Średnia";
                btnAnimHigh.Text = "Wysoka";
                btnAnimUltra.Text = "Ultra";
                btnAnimEpic.Text = "Epicka";
                btnAnimAwesome.Text = "Świetna";
                btnAaDisable.Text = "Wyłącz";

                cbVsync.Text = "Synchronizacja pionowa";
                btnSetFPS.Text = "Ustaw";
                btnResetFPS.Text = "Zresetuj";
                label18.Text = "Ustaw własny limit FPS";

                label22.Text = "Główna głośność";
                label13.Text = "Muzyka w menu";
                label21.Text = "Jakość dźwięku";
                cbHeadphones.Text = "Słuchawki";

                btnAudioLow.Text = "Niska";
                btnAudioMedium.Text = "Średnia";
                btnAudioHigh.Text = "Wysoka";
                btnAudioUltra.Text = "Ultra";
                btnAudioEpic.Text = "Epicka";
                btnAudioAwesome.Text = "Świetna";

                label17.Text = "Wybierz jeden z graficznych presetów";
                btnPresetLow.Text = "Bardzo niskie";
                btnPresetBalanced.Text = "Zbalansowane";
                btnPresetEpic.Text = "Świetne";

                label24.Text = "Zabójca (mysz)";
                label25.Text = "Zabójca (kontroler)";
                label29.Text = "Ocalały (mysz)";
                label27.Text = "Ocalały (kontroler)";
                labelFOV.Text = "Pole widzenia (FOV)";

                lblGameLang.Text = "Język w grze:";
                cbLegacyPrestige.Text = "Portrety Legacy";
                cbSprintToCancel.Text = "Sprint, aby anulować";
                cbHapticsVibration.Text = "Wibracje dotykowe (PS5)";
                cbMuteFocusLost.Text = "Wycisz przy utracie fokusa";
                cbHeartIcon.Text = "Wizualna informacja o promieniu terroru";
            }
            if (Language == "русский")
            {
                errors["confs-dont-exist"] = "Файлы конфигурации Dead by Daylight не существуют! Пожалуйста, запустите игру.";
                errors["new-version"] = "Обнаружена новая версия программы.\n\nВы хотите ее скачать?";
                errors["new-version-fail"] = "Проверка новых версий программы не удалась!";
                errors["max-fps"] = "Максимальная частота кадров в Dead by Daylight - 120. Игра будет отображаться со скоростью 120 кадров в секунду в любом случае. Программа установит вам ограничение ";
                errors["max-fps-continuation"] = " Все равно FPS.";
                errors["delete-confs"] = "Вы уверены, что хотите удалить файл конфигурации и создать новый?";
                errors["delete-confs-title"] = "Удалить резервную копию конфигурации";
                errors["discord-link-fail"] = "Не удалось открыть ссылку Discord!";
                errors["new-version-title"] = "Проверить наличие обновлений";
                errors["information"] = "Информация";
                errors["error"] = "Ошибка";
                errors["fps-error"] = "Создатели Dead by Daylight заблокировали возможность играть с частотой кадров, отличной от: 30, 59, 60, 90, 120\n\nЕсли можно играть на другом кадре оцените, присоединяйтесь к серверу Discord и сообщите нам об этом, и мы удалим это сообщение.";

                errors["tip1"] = "Сбросить настройки до сохраненных в резервной копии.";
                errors["tip2"] = "Сбросить ограничение FPS.";
                errors["tip3"] = "Установите собственное ограничение FPS.";
                errors["tip4"] = "Установите собственное разрешение игры.";
                errors["tip5"] = "Сбросить системное разрешение.";
                errors["tip6"] = "Удалите резервный файл конфигурации и создайте новый.";
                errors["tip7"] = "Официальный сервер Discord.";

                cbCopyright.Text = "Разрешить музыку, защищенную авторскими правами";
                cbSurvivorToggle.Text = "Выживший Переключить режим\n взаимодействия";
                cbKillerToggle.Text = "Убийца Переключить режим\n взаимодействия";
                lblInAppLang.Text = "Язык приложения:";
                lblChangeSettings.Text = "Версия игры:";
                menuAudio.Text = "Аудио";
                menuGraphics.Text = "Графика";
                menuFPS.Text = "FPS";
                menuPresets.Text = "пресеты";
                menuSensitivity.Text = "чувствительность";
                menuGame.Text = "Игра";
                lblWidth.Text = "Ширина";
                lblHeight.Text = "Высота";
                label35.Text = "2D-разрешение";
                btnResSet.Text = "Набор";
                btnResReset.Text = "Перезагрузить";
                label1.Text = "3D-разрешение";
                label3.Text = "Просмотр качества расстояния";
                lblAaQuality.Text = "Качество сглаживания";
                label5.Text = "Качество теней";
                label6.Text = "Качество постобработки";
                label7.Text = "Качество текстур";
                label8.Text = "Качество эффектов";
                label9.Text = "Качество листвы";
                label10.Text = "Качество затенения";
                label11.Text = "Качество анимации";

                btnVwLow.Text = "Низкий";
                btnVwMedium.Text = "Середина";
                btnVwHigh.Text = "Высокий";
                btnVwUltra.Text = "Ультра";
                btnVwEpic.Text = "Эпический";
                btnVwAwesome.Text = "Потрясающий";

                btnAaLow.Text = "Низкий";
                btnAaMedium.Text = "Середина";
                btnAaHigh.Text = "Высокий";
                btnAaUltra.Text = "Ультра";
                btnAaEpic.Text = "Эпический";
                btnAaAwesome.Text = "Потрясающий";

                btnShadLow.Text = "Низкий";
                btnShadMedium.Text = "Середина";
                btnShadHigh.Text = "Высокий";
                btnShadUltra.Text = "Ультра";
                btnShadEpic.Text = "Эпический";
                btnShadAwesome.Text = "Потрясающий";

                btnPpLow.Text = "Низкий";
                btnPpMedium.Text = "Середина";
                btnPpHigh.Text = "Высокий";
                btnPpUltra.Text = "Ультра";
                btnPpEpic.Text = "Эпический";
                btnPpAwesome.Text = "Потрясающий";

                btnTxtLow.Text = "Низкий";
                btnTxtMedium.Text = "Середина";
                btnTxtHigh.Text = "Высокий";
                btnTxtUltra.Text = "Ультра";
                btnTxtEpic.Text = "Эпический";
                btnTxtAwesome.Text = "Потрясающий";

                btnEffLow.Text = "Низкий";
                btnEffMedium.Text = "Середина";
                btnEffHigh.Text = "Высокий";
                btnEffUltra.Text = "Ультра";
                btnEffEpic.Text = "Эпический";
                btnEffAwesome.Text = "Потрясающий";

                btnFolLow.Text = "Низкий";
                btnFolMedium.Text = "Середина";
                btnFolHigh.Text = "Высокий";
                btnFolUltra.Text = "Ультра";
                btnFolEpic.Text = "Эпический";
                btnFolAwesome.Text = "Потрясающий";

                btnShLow.Text = "Низкий";
                btnShMedium.Text = "Середина";
                btnShHigh.Text = "Высокий";
                btnShUltra.Text = "Ультра";
                btnShEpic.Text = "Эпический";
                btnShAwesome.Text = "Потрясающий";

                btnAnimLow.Text = "Низкий";
                btnAnimMedium.Text = "Середина";
                btnAnimHigh.Text = "Высокий";
                btnAnimUltra.Text = "Ультра";
                btnAnimEpic.Text = "Эпический";
                btnAnimAwesome.Text = "Потрясающий";
                btnAaDisable.Text = "Запрещать";
                cbVsync.Text = "вертикальная синхронизация";
                btnSetFPS.Text = "Набор";
                btnResetFPS.Text = "Перезагрузить";
                label18.Text = "Установите собственное ограничение FPS";
                label22.Text = "Основной объем";
                label13.Text = "Меню Музыка";
                label21.Text = "Качество звука";
                cbHeadphones.Text = "Наушники";

                btnAudioLow.Text = "Низкий";
                btnAudioMedium.Text = "Середина";
                btnAudioHigh.Text = "Высокий";
                btnAudioUltra.Text = "Ультра";
                btnAudioEpic.Text = "Эпический";
                btnAudioAwesome.Text = "Потрясающий";

                label17.Text = "Выберите один из графических пресетов.";
                btnPresetLow.Text = "Супер низкий";
                btnPresetBalanced.Text = "выровненный";
                btnPresetEpic.Text = "Потрясающий";

                label24.Text = "Убийца (мышь)";
                label25.Text = "Киллер (контролер)";
                label29.Text = "Выживший (мышь)";
                label27.Text = "Выживший (контроллер)";
                labelFOV.Text = "Поле обзора (FOV)";

                lblGameLang.Text = "Язык игры:";
                cbLegacyPrestige.Text = "Престижные портреты прошлого";
                cbSprintToCancel.Text = "Спринт для отмены";
                cbHapticsVibration.Text = "Тактильная вибрация (PS5)";
                cbMuteFocusLost.Text = "Отключение звука при потере фокуса";
                cbHeartIcon.Text = "Визуальная обратная связь с радиусом террора";
            }
            if (Language == "Deutsch")
            {
                errors["confs-dont-exist"] = "Dead by Daylight Konfigurationsdateien existieren nicht! Bitte starten Sie das Spiel.";
                errors["new-version"] = "Eine neue Version des Programms wurde gefunden.\n\nMöchten Sie sie herunterladen?";
                errors["new-version-fail"] = "Überprüfung neuerer Programmversionen fehlgeschlagen!";
                errors["max-fps"] = "Die maximale Bildrate in Dead by Daylight beträgt 120. Das Spiel wird trotzdem mit 120 Bildern pro Sekunde angezeigt. Das Programm setzt Ihnen ein Limit von ";
                errors["max-fps-continuation"] = " FPS sowieso.";
                errors["delete-confs"] = "Möchten Sie die Konfigurationsdatei wirklich löschen und eine neue erstellen?";
                errors["delete-confs-title"] = "Backup-Konfiguration löschen";
                errors["discord-link-fail"] = "Beim Öffnen des Discord-Links ist ein Problem aufgetreten!";
                errors["new-version-title"] = "Nach Updates suchen";
                errors["information"] = "Informationen";
                errors["error"] = "Fehler";
                errors["fps-error"] = "Die Macher von Dead by Daylight haben die Möglichkeit blockiert, mit anderen Frameraten als 30, 59, 60, 90, 120 zu spielen.\n\nWenn es möglich ist, mit einem anderen Frame zu spielen Bewerten Sie, treten Sie dem Discord-Server bei und melden Sie es uns und wir werden diese Nachricht entfernen.";

                errors["tip1"] = "Einstellungen auf die im Backup gespeicherten zurücksetzen.";
                errors["tip2"] = "FPS-Obergrenze zurücksetzen.";
                errors["tip3"] = "Legen Sie Ihre eigene FPS-Obergrenze fest.";
                errors["tip4"] = "Stellen Sie Ihre eigene Spielauflösung ein.";
                errors["tip5"] = "Auf Systemauflösung zurücksetzen.";
                errors["tip6"] = "Löschen Sie die Backup-Konfigurationsdatei und erstellen Sie eine neue.";
                errors["tip7"] = "Offizieller Discord-Server.";

                cbCopyright.Text = "Urheberrechtlich geschützte Musik zulassen";
                cbSurvivorToggle.Text = "Überlebender Interaktionsmodus umschalten";
                cbKillerToggle.Text = "Mörder Interaktionsmodus umschalten";
                lblInAppLang.Text = "In-App-Sprache:";
                lblChangeSettings.Text = "Spielversion:";
                menuAudio.Text = "Audio";
                menuGraphics.Text = "Grafik";
                menuFPS.Text = "FPS";
                menuPresets.Text = "Voreinstellungen";
                menuSensitivity.Text = "Empfindlichkeit";
                menuGame.Text = "Spiel";
                lblWidth.Text = "Breite";
                lblHeight.Text = "Höhe";
                label35.Text = "2D-Auflösung";
                btnResSet.Text = "Satz";
                btnResReset.Text = "Zurücksetzen";
                label1.Text = "3D-Auflösung";
                label3.Text = "Entfernungsqualität anzeigen";
                lblAaQuality.Text = "Anti-Aliasing-Qualität";
                label5.Text = "Schattenqualität";
                label6.Text = "Qualität der Nachbearbeitung";
                label7.Text = "Qualität der Texturen";
                label8.Text = "Effektqualität";
                label9.Text = "Laubqualität";
                label10.Text = "Schattierungsqualität";
                label11.Text = "Animationsqualität";

                btnVwLow.Text = "Niedrig";
                btnVwMedium.Text = "Mittel";
                btnVwHigh.Text = "Hoch";
                btnVwUltra.Text = "Ultra";
                btnVwEpic.Text = "Episch";
                btnVwAwesome.Text = "Toll";

                btnAaLow.Text = "Niedrig";
                btnAaMedium.Text = "Mittel";
                btnAaHigh.Text = "Hoch";
                btnAaUltra.Text = "Ultra";
                btnAaEpic.Text = "Episch";
                btnAaAwesome.Text = "Toll";

                btnShadLow.Text = "Niedrig";
                btnShadMedium.Text = "Mittel";
                btnShadHigh.Text = "Hoch";
                btnShadUltra.Text = "Ultra";
                btnShadEpic.Text = "Episch";
                btnShadAwesome.Text = "Toll";

                btnPpLow.Text = "Niedrig";
                btnPpMedium.Text = "Mittel";
                btnPpHigh.Text = "Hoch";
                btnPpUltra.Text = "Ultra";
                btnPpEpic.Text = "Episch";
                btnPpAwesome.Text = "Toll";

                btnTxtLow.Text = "Niedrig";
                btnTxtMedium.Text = "Mittel";
                btnTxtHigh.Text = "Hoch";
                btnTxtUltra.Text = "Ultra";
                btnTxtEpic.Text = "Episch";
                btnTxtAwesome.Text = "Toll";

                btnEffLow.Text = "Niedrig";
                btnEffMedium.Text = "Mittel";
                btnEffHigh.Text = "Hoch";
                btnEffUltra.Text = "Ultra";
                btnEffEpic.Text = "Episch";
                btnEffAwesome.Text = "Toll";

                btnFolLow.Text = "Niedrig";
                btnFolMedium.Text = "Mittel";
                btnFolHigh.Text = "Hoch";
                btnFolUltra.Text = "Ultra";
                btnFolEpic.Text = "Episch";
                btnFolAwesome.Text = "Toll";

                btnShLow.Text = "Niedrig";
                btnShMedium.Text = "Mittel";
                btnShHigh.Text = "Hoch";
                btnShUltra.Text = "Ultra";
                btnShEpic.Text = "Episch";
                btnShAwesome.Text = "Toll";

                btnAnimLow.Text = "Niedrig";
                btnAnimMedium.Text = "Mittel";
                btnAnimHigh.Text = "Hoch";
                btnAnimUltra.Text = "Ultra";
                btnAnimEpic.Text = "Episch";
                btnAnimAwesome.Text = "Toll";
                btnAaDisable.Text = "Deaktivieren";
                cbVsync.Text = "Vertikale Synchronisation";
                btnSetFPS.Text = "Satz";
                btnResetFPS.Text = "Zurücksetzen";
                label18.Text = "Legen Sie Ihre eigene FPS-Obergrenze fest";
                label22.Text = "Hauptvolumen";
                label13.Text = "Menü Musik";
                label21.Text = "Audio Qualität";
                cbHeadphones.Text = "Kopfhörer";

                btnAudioLow.Text = "Niedrig";
                btnAudioMedium.Text = "Mittel";
                btnAudioHigh.Text = "Hoch";
                btnAudioUltra.Text = "Ultra";
                btnAudioEpic.Text = "Episch";
                btnAudioAwesome.Text = "Toll";

                label17.Text = "Wählen Sie eine der Grafikvorgaben aus.";
                btnPresetLow.Text = "Sehr niedrig";
                btnPresetBalanced.Text = "Ausgewogen";
                btnPresetEpic.Text = "Toll";

                label24.Text = "Mörder (Maus)";
                label25.Text = "Mörder (Controller)";
                label29.Text = "Überlebender (Maus)";
                label27.Text = "Überlebender (Controller)";
                labelFOV.Text = "Sichtfeld (FOV)";

                lblGameLang.Text = "Spielsprache:";
                cbLegacyPrestige.Text = "Alte Prestige-Portraits";
                cbSprintToCancel.Text = "Sprint zum Abbrechen";
                cbHapticsVibration.Text = "Haptics Vibration (PS5)";
                cbMuteFocusLost.Text = "Mute bei Fokus verloren";
                cbHeartIcon.Text = "Visuelles Feedback zum Terrorradius";
            }
            if (Language == "Français")
            {
                errors["confs-dont-exist"] = "Les fichiers de configuration de Dead by Daylight n'existent pas ! Veuillez lancer le jeu.";
                errors["new-version"] = "Une nouvelle version du programme a été trouvée.\n\nVoulez-vous la télécharger ?";
                errors["new-version-fail"] = "La vérification des nouvelles versions du programme a échoué !";
                errors["max-fps"] = "La fréquence d'images maximale dans Dead by Daylight est de 120. Le jeu sera affiché à 120 images par seconde de toute façon. Le programme vous fixera une limite de ";
                errors["max-fps-continuation"] = " FPS quand même.";
                errors["delete-confs"] = "Êtes-vous sûr de vouloir supprimer le fichier de configuration et en créer un nouveau ?";
                errors["delete-confs-title"] = "Supprimer la configuration de sauvegarde";
                errors["discord-link-fail"] = "Un problème est survenu lors de l'ouverture du lien Discord !";
                errors["new-version-title"] = "Vérifier les mises à jour";
                errors["information"] = "Informations";
                errors["error"] = "Erreur";
                errors["fps-error"] = "Les créateurs de Dead by Daylight ont bloqué la possibilité de jouer à des fréquences d'images autres que : 30, 59, 60, 90, 120\n\nS'il est possible de jouer à une fréquence d'images différente notez, rejoignez le serveur Discord et signalez-le-nous et nous supprimerons ce message.";

                errors["tip1"] = "Réinitialisez les paramètres à ceux enregistrés dans la sauvegarde.";
                errors["tip2"] = "Réinitialiser le plafond FPS.";
                errors["tip3"] = "Définissez votre propre plafond FPS.";
                errors["tip4"] = "Définissez votre propre résolution de jeu.";
                errors["tip5"] = "Réinitialiser à la résolution du système.";
                errors["tip6"] = "Supprimez le fichier de configuration de sauvegarde et créez-en un nouveau.";
                errors["tip7"] = "Serveur Discord officiel.";

                cbCopyright.Text = "Autoriser la musique protégée par le droit d'auteur";
                cbSurvivorToggle.Text = "Survivant bascule le mode d'interactions";
                cbKillerToggle.Text = "Tueur bascule le mode d'interactions";
                lblInAppLang.Text = "Langue:";
                lblChangeSettings.Text = "Version du jeu:";
                menuAudio.Text = "Audio";
                menuGraphics.Text = "Graphiques";
                menuFPS.Text = "FPS";
                menuPresets.Text = "Préréglages";
                menuSensitivity.Text = "Sensibilité";
                menuGame.Text = "Jeu";
                lblWidth.Text = "Largeur";
                lblHeight.Text = "Hauteur";
                label35.Text = "Résolution 2D";
                btnResSet.Text = "Définir";
                btnResReset.Text = "Réinitialiser";
                label1.Text = "Résolution 3D";
                label3.Text = "Afficher la qualité de la distance";
                lblAaQuality.Text = "Qualité anticrénelage";
                label5.Text = "Qualité de l'ombre";
                label6.Text = "Qualité post-traitement";
                label7.Text = "Qualité des textures";
                label8.Text = "Qualité des effets";
                label9.Text = "Qualité du feuillage";
                label10.Text = "Qualité de l'ombrage";
                label11.Text = "Qualité des animations";

                btnVwLow.Text = "Bas";
                btnVwMedium.Text = "Moyen";
                btnVwHigh.Text = "Élevé";
                btnVwUltra.Text = "Ultra";
                btnVwEpic.Text = "Epique";
                btnVwAwesome.Text = "Génial";

                btnAaLow.Text = "Bas";
                btnAaMedium.Text = "Moyen";
                btnAaHigh.Text = "Élevé";
                btnAaUltra.Text = "Ultra";
                btnAaEpic.Text = "Epique";
                btnAaAwesome.Text = "Génial";

                btnShadLow.Text = "Bas";
                btnShadMedium.Text = "Moyen";
                btnShadHigh.Text = "Élevé";
                btnShadUltra.Text = "Ultra";
                btnShadEpic.Text = "Epique";
                btnShadAwesome.Text = "Génial";

                btnPpLow.Text = "Bas";
                btnPpMedium.Text = "Moyen";
                btnPpHigh.Text = "Élevé";
                btnPpUltra.Text = "Ultra";
                btnPpEpic.Text = "Epique";
                btnPpAwesome.Text = "Génial";

                btnTxtLow.Text = "Bas";
                btnTxtMedium.Text = "Moyen";
                btnTxtHigh.Text = "Élevé";
                btnTxtUltra.Text = "Ultra";
                btnTxtEpic.Text = "Epique";
                btnTxtAwesome.Text = "Génial";

                btnEffLow.Text = "Bas";
                btnEffMedium.Text = "Moyen";
                btnEffHigh.Text = "Élevé";
                btnEffUltra.Text = "Ultra";
                btnEffEpic.Text = "Epique";
                btnEffAwesome.Text = "Génial";

                btnFolLow.Text = "Bas";
                btnFolMedium.Text = "Moyen";
                btnFolHigh.Text = "Élevé";
                btnFolUltra.Text = "Ultra";
                btnFolEpic.Text = "Epique";
                btnFolAwesome.Text = "Génial";

                btnShLow.Text = "Bas";
                btnShMedium.Text = "Moyen";
                btnShHigh.Text = "Élevé";
                btnShUltra.Text = "Ultra";
                btnShEpic.Text = "Epique";
                btnShAwesome.Text = "Génial";

                btnAnimLow.Text = "Bas";
                btnAnimMedium.Text = "Moyen";
                btnAnimHigh.Text = "Élevé";
                btnAnimUltra.Text = "Ultra";
                btnAnimEpic.Text = "Epique";
                btnAnimAwesome.Text = "Génial";
                btnAaDisable.Text = "Désactiver";
                cbVsync.Text = "Synchronisation verticale";
                btnSetFPS.Text = "Définir";
                btnResetFPS.Text = "Réinitialiser";
                label18.Text = "Définissez votre propre plafond FPS";
                label22.Text = "Volume principal";
                label13.Text = "Menu Musique";
                label21.Text = "Qualité audio";
                cbHeadphones.Text = "Écouteurs";

                btnAudioLow.Text = "Bas";
                btnAudioMedium.Text = "Moyen";
                btnAudioHigh.Text = "Élevé";
                btnAudioUltra.Text = "Ultra";
                btnAudioEpic.Text = "Epique";
                btnAudioAwesome.Text = "Génial";

                label17.Text = "Choisissez l'un des préréglages graphiques.";
                btnPresetLow.Text = "Super bas";
                btnPresetBalanced.Text = "Balancé";
                btnPresetEpic.Text = "Génial";

                label24.Text = "Tueur (souris)";
                label25.Text = "Tueur (contrôleur)";
                label29.Text = "Survivant (souris)";
                label27.Text = "Survivant (contrôleur)";
                labelFOV.Text = "Champ de vision (FOV)";

                lblGameLang.Text = "Langue du jeu:";
                cbLegacyPrestige.Text = "Portraits de prestige hérités";
                cbSprintToCancel.Text = "Sprint pour annuler";
                cbHapticsVibration.Text = "Vibration haptique (PS5)";
                cbMuteFocusLost.Text = "Mettre en sourdine le focus perdu";
                cbHeartIcon.Text = "Rétroaction visuelle du rayon de terreur";
            }
            if (Language == "日本")
            {
                errors["confs-dont-exist"] = "Dead by Daylightの設定ファイルが存在しません！ゲームを起動してください。";
                errors["new-version"] = "プログラムの新しいバージョンが見つかりました\n\nダウンロードしますか？";
                errors["new-version-fail"] = "新しいプログラムバージョンのチェックに失敗しました！";
                errors["max-fps"] = "Dead by Daylightの最大フレームレートは120です。ゲームはとにかく毎秒120フレームで表示されます。プログラムは の制限を設定します ";
                errors["max-fps-continuation"] = " とにかくFPS";
                errors["delete-confs"] = "構成ファイルを削除して、新しいファイルを作成してもよろしいですか？";
                errors["delete-confs-title"] = "バックアップ構成の削除";
                errors["discord-link-fail"] = "Discordリンクを開くときに問題が発生しました！";
                errors["new-version-title"] = "更新を確認してください";
                errors["information"] = "情報";
                errors["error"] = "エラー";
                errors["fps-error"] = "Dead by Daylight の作成者は、30、59、60、90、120 以外のフレーム レートでプレイする機能をブロックしました\n\n別のフレームでプレイできる場合評価して、Discord サーバーに参加して私たちに報告してください。このメッセージは削除されます。」";

                errors["tip1"] = "バックアップに保存されている設定にリセットします。";
                errors["tip2"] = "FPSキャップをリセットします。";
                errors["tip3"] = "独自のFPSキャップを設定します。";
                errors["tip4"] = "独自のゲーム解像度を設定してください。";
                errors["tip5"] = "システム解像度にリセットします。";
                errors["tip6"] = "バックアップ構成ファイルを削除し、新しいファイルを作成します。";
                errors["tip7"] = "公式Discordサーバー。";

                cbCopyright.Text = "著作権で保護された音楽を許可する";
                cbSurvivorToggle.Text = "サバイバートグルインタラクションモード";
                cbKillerToggle.Text = "キラートグルインタラクションモード";
                lblInAppLang.Text = "アプリ内言語：";
                lblChangeSettings.Text = "ゲームバージョン：";
                menuAudio.Text = "オーディオ";
                menuGraphics.Text = "グラフィックス";
                menuFPS.Text = "FPSキャップ";
                menuPresets.Text = "グラフィックプリセット";
                menuSensitivity.Text = "マウスの感度";
                menuGame.Text = "試合";
                lblWidth.Text = "幅";
                lblHeight.Text = "高さ";
                label35.Text = "2D解像度";
                btnResSet.Text = "設定";
                btnResReset.Text = "リセット";
                label1.Text = "3D解像度";
                label3.Text = "距離の品質を表示";
                lblAaQuality.Text = "アンチエイリアシング品質";
                label5.Text = "シャドウ品質";
                label6.Text = "後処理品質";
                label7.Text = "テクスチャ品質";
                label8.Text = "効果の品質";
                label9.Text = "葉の品質";
                label10.Text = "シェーディング品質";
                label11.Text = "アニメーションの品質";

                btnVwLow.Text = "低";
                btnVwMedium.Text = "中";
                btnVwHigh.Text = "高";
                btnVwUltra.Text = "ウルトラ";
                btnVwEpic.Text = "大作";
                btnVwAwesome.Text = "素晴らしい";

                btnAaLow.Text = "低";
                btnAaMedium.Text = "中";
                btnAaHigh.Text = "高";
                btnAaUltra.Text = "ウルトラ";
                btnAaEpic.Text = "大作";
                btnAaAwesome.Text = "素晴らしい";

                btnShadLow.Text = "低";
                btnShadMedium.Text = "中";
                btnShadHigh.Text = "高";
                btnShadUltra.Text = "ウルトラ";
                btnShadEpic.Text = "大作";
                btnShadAwesome.Text = "素晴らしい";

                btnPpLow.Text = "低";
                btnPpMedium.Text = "中";
                btnPpHigh.Text = "高";
                btnPpUltra.Text = "ウルトラ";
                btnPpEpic.Text = "大作";
                btnPpAwesome.Text = "素晴らしい";

                btnTxtLow.Text = "低";
                btnTxtMedium.Text = "中";
                btnTxtHigh.Text = "高";
                btnTxtUltra.Text = "ウルトラ";
                btnTxtEpic.Text = "大作";
                btnTxtAwesome.Text = "素晴らしい";

                btnEffLow.Text = "低";
                btnEffMedium.Text = "中";
                btnEffHigh.Text = "高";
                btnEffUltra.Text = "ウルトラ";
                btnEffEpic.Text = "大作";
                btnEffAwesome.Text = "素晴らしい";

                btnFolLow.Text = "低";
                btnFolMedium.Text = "中";
                btnFolHigh.Text = "高";
                btnFolUltra.Text = "ウルトラ";
                btnFolEpic.Text = "大作";
                btnFolAwesome.Text = "素晴らしい";

                btnShLow.Text = "低";
                btnShMedium.Text = "中";
                btnShHigh.Text = "高";
                btnShUltra.Text = "ウルトラ";
                btnShEpic.Text = "大作";
                btnShAwesome.Text = "素晴らしい";

                btnAnimLow.Text = "低";
                btnAnimMedium.Text = "中";
                btnAnimHigh.Text = "高";
                btnAnimUltra.Text = "ウルトラ";
                btnAnimEpic.Text = "大作";
                btnAnimAwesome.Text = "素晴らしい";
                btnAaDisable.Text = "無効";
                cbVsync.Text = "垂直同期";
                btnSetFPS.Text = "設定";
                btnResetFPS.Text = "リセット";
                label18.Text = "独自のFPSキャップを設定";
                label22.Text = "メインボリューム";
                label13.Text = "メニュー音楽";
                label21.Text = "オーディオ品質";
                cbHeadphones.Text = "ヘッドフォン";

                btnAudioLow.Text = "低";
                btnAudioMedium.Text = "中";
                btnAudioHigh.Text = "高";
                btnAudioUltra.Text = "ウルトラ";
                btnAudioEpic.Text = "大作";
                btnAudioAwesome.Text = "素晴らしい";

                label17.Text = "グラフィックプリセットの1つを選択してください。";
                btnPresetLow.Text = "超低";
                btnPresetBalanced.Text = "バランスの取れた";
                btnPresetEpic.Text = "素晴らしい";

                label24.Text = "キラー（マウス）";
                label25.Text = "キラー（コントローラー）";
                label29.Text = "サバイバー（マウス）";
                label27.Text = "サバイバー（コントローラー）";
                labelFOV.Text = "視野 (FOV)";

                lblGameLang.Text = "ゲーム内言語:";
                cbLegacyPrestige.Text = "レガシープレステージポートレート";
                cbSprintToCancel.Text = "スプリントからキャンセル";
                cbHapticsVibration.Text = "触覚振動(PS5)";
                cbMuteFocusLost.Text = "フォーカスが失われたときにミュート";
                cbHeartIcon.Text = "Terror Radius ビジュアル フィードバック";
            }
            if (Language == "中国人")
            {
                errors["confs-dont-exist"] = "Dead by Daylight 配置文件不存在！请启动游戏。";
                errors["new-version"] = "已找到该程序的新版本。\n\n您要下载吗？";
                errors["new-version-fail"] = "检查较新的程序版本失败！";
                errors["max-fps"] = "Dead by Daylight 中的最大帧速率为 120。游戏将以每秒 120 帧的速度显示。程序将为您设置一个限制 ";
                errors["max-fps-continuation"] = " FPS 无论如何。";
                errors["delete-confs"] = "您确定要删除配置文件并新建一个吗？";
                errors["delete-confs-title"] = "删除备份配置";
                errors["discord-link-fail"] = "打开 Discord 链接时出现问题！";
                errors["new-version-title"] = "检查更新";
                errors["information"] = "信息";
                errors["error"] = "错误";
                errors["fps-error"] = "《黎明杀机》的制作者已经阻止了以以下帧速率进行游戏的能力：30、59、60、90、120\n\n如果可以以不同的帧进行游戏率，加入 Discord 服务器并向我们报告，我们将删除此消息。";

                errors["tip1"] = "将设置重置为备份中保存的设置。";
                errors["tip2"] = "重置 FPS 上限。";
                errors["tip3"] = "设置你自己的 FPS 上限。";
                errors["tip4"] = "设置你自己的游戏分辨率。";
                errors["tip5"] = "重置为系统分辨率。";
                errors["tip6"] = "删除备份配置文件并新建一个。";
                errors["tip7"] = "官方 Discord 服务器。";

                cbCopyright.Text = "允许受版权保护的音乐";
                cbSurvivorToggle.Text = "幸存者切换交互模式";
                cbKillerToggle.Text = "杀手切换交互模式";
                lblInAppLang.Text = "应用内语言：";
                lblChangeSettings.Text = "游戏版本：";
                menuAudio.Text = "音频";
                menuGraphics.Text = "图形";
                menuFPS.Text = "帧数限制";
                menuPresets.Text = "图形预设";
                menuSensitivity.Text = "鼠标灵敏度";
                menuGame.Text = "游戏";
                lblWidth.Text = "宽度";
                lblHeight.Text = "高度";
                label35.Text = "二维分辨率";
                btnResSet.Text = "设置";
                btnResReset.Text = "重置";
                label1.Text = "3D 分辨率";
                label3.Text = "查看距离质量";
                lblAaQuality.Text = "抗锯齿质量";
                label5.Text = "阴影质量";
                label6.Text = "后处理质量";
                label7.Text = "纹理质量";
                label8.Text = "影响质量";
                label9.Text = "树叶质量";
                label10.Text = "着色质量";
                label11.Text = "动画质量";

                btnVwLow.Text = "低";
                btnVwMedium.Text = "中等";
                btnVwHigh.Text = "高";
                btnVwUltra.Text = "超";
                btnVwEpic.Text = "史诗";
                btnVwAwesome.Text = "真棒";

                btnAaLow.Text = "低";
                btnAaMedium.Text = "中等";
                btnAaHigh.Text = "高";
                btnAaUltra.Text = "超";
                btnAaEpic.Text = "史诗";
                btnAaAwesome.Text = "真棒";

                btnShadLow.Text = "低";
                btnShadMedium.Text = "中等";
                btnShadHigh.Text = "高";
                btnShadUltra.Text = "超";
                btnShadEpic.Text = "史诗";
                btnShadAwesome.Text = "真棒";

                btnPpLow.Text = "低";
                btnPpMedium.Text = "中等";
                btnPpHigh.Text = "高";
                btnPpUltra.Text = "超";
                btnPpEpic.Text = "史诗";
                btnPpAwesome.Text = "真棒";

                btnTxtLow.Text = "低";
                btnTxtMedium.Text = "中等";
                btnTxtHigh.Text = "高";
                btnTxtUltra.Text = "超";
                btnTxtEpic.Text = "史诗";
                btnTxtAwesome.Text = "真棒";

                btnEffLow.Text = "低";
                btnEffMedium.Text = "中等";
                btnEffHigh.Text = "高";
                btnEffUltra.Text = "超";
                btnEffEpic.Text = "史诗";
                btnEffAwesome.Text = "真棒";

                btnFolLow.Text = "低";
                btnFolMedium.Text = "中等";
                btnFolHigh.Text = "高";
                btnFolUltra.Text = "超";
                btnFolEpic.Text = "史诗";
                btnFolAwesome.Text = "真棒";

                btnShLow.Text = "低";
                btnShMedium.Text = "中等";
                btnShHigh.Text = "高";
                btnShUltra.Text = "超";
                btnShEpic.Text = "史诗";
                btnShAwesome.Text = "真棒";

                btnAnimLow.Text = "低";
                btnAnimMedium.Text = "中等";
                btnAnimHigh.Text = "高";
                btnAnimUltra.Text = "超";
                btnAnimEpic.Text = "史诗";
                btnAnimAwesome.Text = "真棒";
                btnAaDisable.Text = "禁用";
                cbVsync.Text = "垂直同步";
                btnSetFPS.Text = "设置";
                btnResetFPS.Text = "重置";
                label18.Text = "设置你自己的 FPS 上限";
                label22.Text = "主卷";
                label13.Text = "菜单音乐";
                label21.Text = "音频质量";
                cbHeadphones.Text = "耳机";

                btnAudioLow.Text = "低";
                btnAudioMedium.Text = "中等";
                btnAudioHigh.Text = "高";
                btnAudioUltra.Text = "超";
                btnAudioEpic.Text = "史诗";
                btnAudioAwesome.Text = "真棒";

                label17.Text = "选择图形预设之一。";
                btnPresetLow.Text = "超低";
                btnPresetBalanced.Text = "均衡";
                btnPresetEpic.Text = "真棒";

                label24.Text = "杀手（鼠标）";
                label25.Text = "杀手（控制器）";
                label29.Text = "幸存者（鼠标）";
                label27.Text = "幸存者（控制器）";
                labelFOV.Text = "视野（FOV）";

                lblGameLang.Text = "游戏内语言：";
                cbLegacyPrestige.Text = "传统威望肖像";
                cbSprintToCancel.Text = "冲刺取消";
                cbHapticsVibration.Text = "触觉振动(PS5)";
                cbMuteFocusLost.Text = "焦点丢失静音";
                cbHeartIcon.Text = "恐怖半径视觉反馈";
            }
            if (Language == "Türkçe")
            {
                errors["confs-dont-exist"] = "Dead by Daylight yapılandırma dosyaları mevcut değil! Lütfen oyunu başlatın.";
                errors["new-version"] = "Programın yeni bir sürümü bulundu.\n\nİndirmek istiyor musunuz?";
                errors["new-version-fail"] = "Daha yeni program sürümleri kontrol edilemedi!";
                errors["max-fps"] = "Dead by Daylight'ta maksimum kare hızı 120'dir. Oyun zaten 120 kare/saniye olarak gösterilecektir. Program size bir limit belirleyecektir ";
                errors["max-fps-continuation"] = " FPS yine de.";
                errors["delete-confs"] = "Yapılandırma dosyasını silip yeni bir tane oluşturmak istediğinizden emin misiniz?";
                errors["delete-confs-title"] = "Yedekleme yapılandırmasını sil";
                errors["discord-link-fail"] = "Discord bağlantısı açılırken bir sorun oluştu!";
                errors["new-version-title"] = "Güncellemeleri kontrol et";
                errors["information"] = "Bilgi";
                errors["error"] = "Hata";
                errors["fps-error"] = "Dead by Daylight'ın yaratıcıları aşağıdakiler dışındaki kare hızlarında oynatma özelliğini engellediler: 30, 59, 60, 90, 120\n\nFarklı bir karede oynamak mümkünse değerlendirin, Discord sunucusuna katılın ve bize bildirin, biz de bu mesajı kaldıralım.";

                errors["tip1"] = "Ayarları yedekte kayıtlı olanlara sıfırlayın.";
                errors["tip2"] = "FPS sınırını sıfırla.";
                errors["tip3"] = "Kendi FPS sınırınızı belirleyin.";
                errors["tip4"] = "Kendi oyun çözünürlüğünüzü ayarlayın.";
                errors["tip5"] = "Sistem çözünürlüğüne sıfırla.";
                errors["tip6"] = "Yedek yapılandırma dosyasını silin ve yeni bir tane oluşturun.";
                errors["tip7"] = "Resmi Discord Sunucusu.";

                cbCopyright.Text = "Telif hakkıyla korunan müziğe izin ver";
                cbSurvivorToggle.Text = "Hayatta Kalan Etkileşimler Modunu Değiştir";
                cbKillerToggle.Text = "Killer Geçiş Etkileşimleri Modu";
                lblInAppLang.Text = "Uygulama içi dil:";
                lblChangeSettings.Text = "Oyun sürümü:";
                menuAudio.Text = "Ses";
                menuGraphics.Text = "Grafikler";
                menuFPS.Text = "FPS Sınırı";
                menuPresets.Text = "Grafik ön ayarları";
                menuSensitivity.Text = "Fare duyarlılığı";
                menuGame.Text = "Oyun";
                lblWidth.Text = "Genişlik";
                lblHeight.Text = "Yükseklik";
                label35.Text = "2D Çözünürlük";
                btnResSet.Text = "Ayarla";
                btnResReset.Text = "Sıfırla";
                label1.Text = "3D Çözünürlük";
                label3.Text = "Mesafe Kalitesini Görüntüle";
                lblAaQuality.Text = "Örtüşme Önleme Kalitesi";
                label5.Text = "Gölge Kalitesi";
                label6.Text = "İşlem Sonrası Kalite";
                label7.Text = "Doku Kalitesi";
                label8.Text = "Etki Kalitesi";
                label9.Text = "Yaprak Kalitesi";
                label10.Text = "Gölgeleme Kalitesi";
                label11.Text = "Animasyon Kalitesi";

                btnVwLow.Text = "Düşük";
                btnVwMedium.Text = "Orta";
                btnVwHigh.Text = "Yüksek";
                btnVwUltra.Text = "Ultra";
                btnVwEpic.Text = "Epik";
                btnVwAwesome.Text = "Harika";

                btnAaLow.Text = "Düşük";
                btnAaMedium.Text = "Orta";
                btnAaHigh.Text = "Yüksek";
                btnAaUltra.Text = "Ultra";
                btnAaEpic.Text = "Epik";
                btnAaAwesome.Text = "Harika";

                btnShadLow.Text = "Düşük";
                btnShadMedium.Text = "Orta";
                btnShadHigh.Text = "Yüksek";
                btnShadUltra.Text = "Ultra";
                btnShadEpic.Text = "Epik";
                btnShadAwesome.Text = "Harika";

                btnPpLow.Text = "Düşük";
                btnPpMedium.Text = "Orta";
                btnPpHigh.Text = "Yüksek";
                btnPpUltra.Text = "Ultra";
                btnPpEpic.Text = "Epik";
                btnPpAwesome.Text = "Harika";

                btnTxtLow.Text = "Düşük";
                btnTxtMedium.Text = "Orta";
                btnTxtHigh.Text = "Yüksek";
                btnTxtUltra.Text = "Ultra";
                btnTxtEpic.Text = "Epik";
                btnTxtAwesome.Text = "Harika";

                btnEffLow.Text = "Düşük";
                btnEffMedium.Text = "Orta";
                btnEffHigh.Text = "Yüksek";
                btnEffUltra.Text = "Ultra";
                btnEffEpic.Text = "Epik";
                btnEffAwesome.Text = "Harika";

                btnFolLow.Text = "Düşük";
                btnFolMedium.Text = "Orta";
                btnFolHigh.Text = "Yüksek";
                btnFolUltra.Text = "Ultra";
                btnFolEpic.Text = "Epik";
                btnFolAwesome.Text = "Harika";

                btnShLow.Text = "Düşük";
                btnShMedium.Text = "Orta";
                btnShHigh.Text = "Yüksek";
                btnShUltra.Text = "Ultra";
                btnShEpic.Text = "Epik";
                btnShAwesome.Text = "Harika";

                btnAnimLow.Text = "Düşük";
                btnAnimMedium.Text = "Orta";
                btnAnimHigh.Text = "Yüksek";
                btnAnimUltra.Text = "Ultra";
                btnAnimEpic.Text = "Epik";
                btnAnimAwesome.Text = "Harika";
                btnAaDisable.Text = "Devre Dışı Bırak";
                cbVsync.Text = "Dikey senkronizasyon";
                btnSetFPS.Text = "Ayarla";
                btnResetFPS.Text = "Sıfırla";
                label18.Text = "Kendi FPS sınırınızı belirleyin";
                label22.Text = "Ana Cilt";
                label13.Text = "Menü Müziği";
                label21.Text = "Ses Kalitesi";
                cbHeadphones.Text = "Kulaklıklar";

                btnAudioLow.Text = "Düşük";
                btnAudioMedium.Text = "Orta";
                btnAudioHigh.Text = "Yüksek";
                btnAudioUltra.Text = "Ultra";
                btnAudioEpic.Text = "Epik";
                btnAudioAwesome.Text = "Harika";

                label17.Text = "Grafik ön ayarlarından birini seçin.";
                btnPresetLow.Text = "Süper düşük";
                btnPresetBalanced.Text = "Dengeli";
                btnPresetEpic.Text = "Harika";

                label24.Text = "Katil (fare)";
                label25.Text = "Killer (denetleyici)";
                label29.Text = "Hayatta kalan (fare)";
                label27.Text = "Hayatta kalan (kontrolör)";
                labelFOV.Text = "Görüş alanı (FOV)";

                lblGameLang.Text = "Oyun içi dil:";
                cbLegacyPrestige.Text = "Eski Prestij Portreleri";
                cbSprintToCancel.Text = "İptal etmek için Sprint";
                cbHapticsVibration.Text = "Dokunsal Titreşim (PS5)";
                cbMuteFocusLost.Text = "Odak kaybolduğunda sessize al";
                cbHeartIcon.Text = "Terör Yarıçapı Görsel Geri Bildirimi";
            }
            if (Language == "Español")
            {
                errors["confs-dont-exist"] = "¡Los archivos de configuración de Dead by Daylight no existen! Por favor, inicia el juego.";
                errors["new-version"] = "Se ha encontrado una nueva versión del programa.\n\n¿Desea descargarla?";
                errors["new-version-fail"] = "¡Error al comprobar las versiones más recientes del programa!";
                errors["max-fps"] = "La velocidad de fotogramas máxima en Dead by Daylight es 120. El juego se mostrará a 120 fotogramas por segundo de todos modos. El programa establecerá un límite de ";
                errors["max-fps-continuation"] = " FPS de todos modos.";
                errors["delete-confs"] = "¿Está seguro de que desea eliminar el archivo de configuración y crear uno nuevo?";
                errors["delete-confs-title"] = "Eliminar configuración de copia de seguridad";
                errors["discord-link-fail"] = "¡Hubo un problema al abrir el enlace de Discord!";
                errors["new-version-title"] = "Buscar actualizaciones";
                errors["information"] = "Información";
                errors["error"] = "Error";
                errors["fps-error"] = "Los creadores de Dead by Daylight han bloqueado la posibilidad de jugar a velocidades de fotogramas distintas a: 30, 59, 60, 90, 120\n\nSi es posible jugar en un fotograma diferente califica, únete al servidor de Discord e infórmanos y eliminaremos este mensaje.";

                errors["tip1"] = "Restablecer la configuración a la guardada en la copia de seguridad.";
                errors["tip2"] = "Restablecer límite de FPS.";
                errors["tip3"] = "Establece tu propio límite de FPS.";
                errors["tip4"] = "Establece tu propia resolución de juego.";
                errors["tip5"] = "Restablecer a la resolución del sistema.";
                errors["tip6"] = "Elimine el archivo de configuración de la copia de seguridad y cree uno nuevo.";
                errors["tip7"] = "Servidor oficial de Discord.";

                cbCopyright.Text = "Permitir música con derechos de autor";
                cbSurvivorToggle.Text = "Sobreviviente Alternar modo de interacciones";
                cbKillerToggle.Text = "Asesino Alternar modo de interacciones";
                lblInAppLang.Text = "Idioma:";
                lblChangeSettings.Text = "Versión del juego:";
                menuAudio.Text = "Audio";
                menuGraphics.Text = "Gráficos";
                menuFPS.Text = "FPS";
                menuPresets.Text = "Presets gráficos";
                menuSensitivity.Text = "Sensibilidad";
                menuGame.Text = "Juego";
                lblWidth.Text = "Ancho";
                lblHeight.Text = "Altura";
                label35.Text = "Resolución 2D";
                btnResSet.Text = "Establecer";
                btnResReset.Text = "Reiniciar";
                label1.Text = "Resolución 3D";
                label3.Text = "Ver calidad de distancia";
                lblAaQuality.Text = "Calidad de suavizado";
                label5.Text = "Calidad de la sombra";
                label6.Text = "Calidad de posprocesamiento";
                label7.Text = "Calidad de las texturas";
                label8.Text = "Calidad de los efectos";
                label9.Text = "Calidad del follaje";
                label10.Text = "Calidad de sombreado";
                label11.Text = "Calidad de las animaciones";

                btnVwLow.Text = "Bajo";
                btnVwMedium.Text = "Medio";
                btnVwHigh.Text = "Alto";
                btnVwUltra.Text = "Ultra";
                btnVwEpic.Text = "Épico";
                btnVwAwesome.Text = "Impresionante";

                btnAaLow.Text = "Bajo";
                btnAaMedium.Text = "Medio";
                btnAaHigh.Text = "Alto";
                btnAaUltra.Text = "Ultra";
                btnAaEpic.Text = "Épico";
                btnAaAwesome.Text = "Impresionante";

                btnShadLow.Text = "Bajo";
                btnShadMedium.Text = "Medio";
                btnShadHigh.Text = "Alto";
                btnShadUltra.Text = "Ultra";
                btnShadEpic.Text = "Épico";
                btnShadAwesome.Text = "Impresionante";

                btnPpLow.Text = "Bajo";
                btnPpMedium.Text = "Medio";
                btnPpHigh.Text = "Alto";
                btnPpUltra.Text = "Ultra";
                btnPpEpic.Text = "Épico";
                btnPpAwesome.Text = "Impresionante";

                btnTxtLow.Text = "Bajo";
                btnTxtMedium.Text = "Medio";
                btnTxtHigh.Text = "Alto";
                btnTxtUltra.Text = "Ultra";
                btnTxtEpic.Text = "Épico";
                btnTxtAwesome.Text = "Impresionante";

                btnEffLow.Text = "Bajo";
                btnEffMedium.Text = "Medio";
                btnEffHigh.Text = "Alto";
                btnEffUltra.Text = "Ultra";
                btnEffEpic.Text = "Épico";
                btnEffAwesome.Text = "Impresionante";

                btnFolLow.Text = "Bajo";
                btnFolMedium.Text = "Medio";
                btnFolHigh.Text = "Alto";
                btnFolUltra.Text = "Ultra";
                btnFolEpic.Text = "Épico";
                btnFolAwesome.Text = "Impresionante";

                btnShLow.Text = "Bajo";
                btnShMedium.Text = "Medio";
                btnShHigh.Text = "Alto";
                btnShUltra.Text = "Ultra";
                btnShEpic.Text = "Épico";
                btnShAwesome.Text = "Impresionante";

                btnAnimLow.Text = "Bajo";
                btnAnimMedium.Text = "Medio";
                btnAnimHigh.Text = "Alto";
                btnAnimUltra.Text = "Ultra";
                btnAnimEpic.Text = "Épico";
                btnAnimAwesome.Text = "Impresionante";
                btnAaDisable.Text = "Deshabilitar";
                cbVsync.Text = "Sincronización vertical";
                btnSetFPS.Text = "Establecer";
                btnResetFPS.Text = "Reiniciar";
                label18.Text = "Establece tu propio límite de FPS";
                label22.Text = "Volumen principal";
                label13.Text = "Menú Música";
                label21.Text = "Calidad de audio";
                cbHeadphones.Text = "Auriculares";

                btnAudioLow.Text = "Bajo";
                btnAudioMedium.Text = "Medio";
                btnAudioHigh.Text = "Alto";
                btnAudioUltra.Text = "Ultra";
                btnAudioEpic.Text = "Épico";
                btnAudioAwesome.Text = "Impresionante";

                label17.Text = "Elija uno de los ajustes preestablecidos gráficos.";
                btnPresetLow.Text = "Muy bajo";
                btnPresetBalanced.Text = "Balanceado";
                btnPresetEpic.Text = "Impresionante";

                label24.Text = "Asesino (ratón)";
                label25.Text = "Asesino (controlador)";
                label29.Text = "Superviviente (ratón)";
                label27.Text = "Superviviente (controlador)";
                labelFOV.Text = "Campo de visión (FOV)";

                lblGameLang.Text = "Idioma del juego:";
                cbLegacyPrestige.Text = "Retratos heredados de prestigio";
                cbSprintToCancel.Text = "Sprint para cancelar";
                cbHapticsVibration.Text = "Vibración háptica (PS5)";
                cbMuteFocusLost.Text = "Silencio al perder el foco";
                cbHeartIcon.Text = "Retroalimentación visual del radio de terror";
            }
            if (Language == "Italiano")
            {
                errors["confs-dont-exist"] = "I file di configurazione di Dead by Daylight non esistono! Per favore, avvia il gioco.";
                errors["new-version"] = "È stata trovata una nuova versione del programma.\n\nVuoi scaricarla?";
                errors["new-version-fail"] = "Controllo delle versioni più recenti del programma fallito!";
                errors["max-fps"] = "Il frame rate massimo in Dead by Daylight è 120. Il gioco verrà comunque visualizzato a 120 frame al secondo. Il programma ti imposterà un limite di ";
                errors["max-fps-continuation"] = " FPS comunque.";
                errors["delete-confs"] = "Sei sicuro di voler eliminare il file di configurazione e crearne uno nuovo?";
                errors["delete-confs-title"] = "Elimina configurazione di backup";
                errors["discord-link-fail"] = "Si è verificato un problema durante l'apertura del collegamento Discord!";
                errors["new-version-title"] = "Verifica aggiornamenti";
                errors["information"] = "Informazioni";
                errors["error"] = "Errore";
                errors["fps-error"] = "I creatori di Dead by Daylight hanno bloccato la possibilità di giocare a frame rate diversi da: 30, 59, 60, 90, 120\n\nSe è possibile giocare a un frame diverso vota, unisciti al server Discord e segnalacelo e rimuoveremo questo messaggio.";

                errors["tip1"] = "Ripristina le impostazioni su quelle salvate nel backup.";
                errors["tip2"] = "Reimposta limite FPS.";
                errors["tip3"] = "Imposta il tuo limite FPS.";
                errors["tip4"] = "Imposta la tua risoluzione di gioco.";
                errors["tip5"] = "Ripristina risoluzione di sistema.";
                errors["tip6"] = "Elimina il file di configurazione del backup e creane uno nuovo.";
                errors["tip7"] = "Server Discord ufficiale.";

                cbCopyright.Text = "Consenti musica protetta da copyright";
                cbSurvivorToggle.Text = "Sopravvissuto attiva/disattiva \nla modalità di interazione";
                cbKillerToggle.Text = "Il killer attiva/disattiva \nla modalità di interazione";
                lblInAppLang.Text = "Lingua nell'app:";
                lblChangeSettings.Text = "Versione del gioco:";
                menuAudio.Text = "Audio";
                menuGraphics.Text = "Grafica";
                menuFPS.Text = "Tappo FPS";
                menuPresets.Text = "Preimpostazioni grafiche";
                menuSensitivity.Text = "Sensibilità del mouse";
                menuGame.Text = "Gioco";
                lblWidth.Text = "Larghezza";
                lblHeight.Text = "Altezza";
                label35.Text = "Risoluzione 2D";
                btnResSet.Text = "Imposta";
                btnResReset.Text = "Ripristina";
                label1.Text = "Risoluzione 3D";
                label3.Text = "Visualizza la qualità della distanza";
                lblAaQuality.Text = "Qualità anti-aliasing";
                label5.Text = "Qualità ombra";
                label6.Text = "Qualità post-elaborazione";
                label7.Text = "Qualità delle trame";
                label8.Text = "Qualità degli effetti";
                label9.Text = "Qualità fogliame";
                label10.Text = "Qualità dell'ombreggiatura";
                label11.Text = "Qualità animazioni";

                btnVwLow.Text = "Basso";
                btnVwMedium.Text = "Medio";
                btnVwHigh.Text = "Alto";
                btnVwUltra.Text = "Ultra";
                btnVwEpic.Text = "Epico";
                btnVwAwesome.Text = "Fantastico";

                btnAaLow.Text = "Basso";
                btnAaMedium.Text = "Medio";
                btnAaHigh.Text = "Alto";
                btnAaUltra.Text = "Ultra";
                btnAaEpic.Text = "Epico";
                btnAaAwesome.Text = "Fantastico";

                btnShadLow.Text = "Basso";
                btnShadMedium.Text = "Medio";
                btnShadHigh.Text = "Alto";
                btnShadUltra.Text = "Ultra";
                btnShadEpic.Text = "Epico";
                btnShadAwesome.Text = "Fantastico";

                btnPpLow.Text = "Basso";
                btnPpMedium.Text = "Medio";
                btnPpHigh.Text = "Alto";
                btnPpUltra.Text = "Ultra";
                btnPpEpic.Text = "Epico";
                btnPpAwesome.Text = "Fantastico";

                btnTxtLow.Text = "Basso";
                btnTxtMedium.Text = "Medio";
                btnTxtHigh.Text = "Alto";
                btnTxtUltra.Text = "Ultra";
                btnTxtEpic.Text = "Epico";
                btnTxtAwesome.Text = "Fantastico";

                btnEffLow.Text = "Basso";
                btnEffMedium.Text = "Medio";
                btnEffHigh.Text = "Alto";
                btnEffUltra.Text = "Ultra";
                btnEffEpic.Text = "Epico";
                btnEffAwesome.Text = "Fantastico";

                btnFolLow.Text = "Basso";
                btnFolMedium.Text = "Medio";
                btnFolHigh.Text = "Alto";
                btnFolUltra.Text = "Ultra";
                btnFolEpic.Text = "Epico";
                btnFolAwesome.Text = "Fantastico";

                btnShLow.Text = "Basso";
                btnShMedium.Text = "Medio";
                btnShHigh.Text = "Alto";
                btnShUltra.Text = "Ultra";
                btnShEpic.Text = "Epico";
                btnShAwesome.Text = "Fantastico";

                btnAnimLow.Text = "Basso";
                btnAnimMedium.Text = "Medio";
                btnAnimHigh.Text = "Alto";
                btnAnimUltra.Text = "Ultra";
                btnAnimEpic.Text = "Epico";
                btnAnimAwesome.Text = "Fantastico";
                btnAaDisable.Text = "Disabilita";
                cbVsync.Text = "Sincronizzazione verticale";
                btnSetFPS.Text = "Imposta";
                btnResetFPS.Text = "Ripristina";
                label18.Text = "Imposta il tuo limite FPS";
                label22.Text = "Volume principale";
                label13.Text = "Menu Musica";
                label21.Text = "Qualità audio";
                cbHeadphones.Text = "Cuffie";

                btnAudioLow.Text = "Basso";
                btnAudioMedium.Text = "Medio";
                btnAudioHigh.Text = "Alto";
                btnAudioUltra.Text = "Ultra";
                btnAudioEpic.Text = "Epico";
                btnAudioAwesome.Text = "Fantastico";

                label17.Text = "Scegli uno dei predefiniti grafici.";
                btnPresetLow.Text = "Super basso";
                btnPresetBalanced.Text = "Bilanciato";
                btnPresetEpic.Text = "Fantastico";

                label24.Text = "Il killer (mouse)";
                label25.Text = "Il killer (controllore)";
                label29.Text = "Sopravvissuto (mouse)";
                label27.Text = "Sopravvissuto (controllore)";
                labelFOV.Text = "Campo visivo (FOV)";

                lblGameLang.Text = "Lingua di gioco:";
                cbLegacyPrestige.Text = "Ritratti di prestigio dell'eredità";
                cbSprintToCancel.Text = "Sprint per annullare";
                cbHapticsVibration.Text = "Vibrazione aptica (PS5)";
                cbMuteFocusLost.Text = "Disattiva messa a fuoco persa";
                cbHeartIcon.Text = "Feedback visivo raggio di terrore";
            }
        }
    }
}
