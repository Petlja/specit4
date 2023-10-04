Скрипт парадигма
----------------

**Скрипт језици** су посебно дизајнирани програмски језици који се
користе за писање **скриптова**. Скрипт језици оригинално нису били
намењени програмирању самосталних апликација, већ је основни циљ
скриптова био да се коришћењем програмирања постојеће апликације
подесе, прилагоде и прошире неком новом функционалношћу као и да
допринесу аутоматизацију разних задатака. То значи да се скриптови не
компилирају већ се интерпретирају (често из система који проширују).
Ова парадигма се истиче својом једноставношћу и брзином развоја,
чинећи је идеалним алатом за администрирање система, веб-програмирање,
анализу података и многе друге области.

Скриптови на вебу
.................

На пример, језик **JavaScript** је био намењен проширивању могућности
веб-прегледача тако да се до тада пасивним веб-страницама може додати
могућност аутоматског генерисања одређеног садржаја и интеракције са
корисницима. Иако се скриптови написани у језику JavaScript и даље
најчешће извршавају у склопу прегледача веба (мада постоје платформе
потпут Node.js које омогућавају самостално извршавање JavaScript
скриптова на веб-серверу, ван прегледача), ови скриптови су постали
толико важни на савременом вебу, али и толико комплексни да се данас
често сматрају самосталним веб-апликацијама.


.. infonote::
   
   Размотримо један мали пример скрипта написаног у језику JavaScript (о
   овом језику ћете детаљније учити у курсу веб-програмирање).
    
   .. code-block:: html
    
      <p id="pasus">Tekst u ovom pasusu se može sakriti.</p>
      <button id="dugme">Sakrij pasus</button>
      <script type="text/javascript">
         const dugme = document.getElementById("dugme");
         dugme.getEventListener("click", function(event) {
               const pasus = document.getElementById("pasus");
               pasus.style.display = "none";
         });
      </script>
    
   Веб-страница садржи пасус и дугме. Када корисник притиска дугме,
   пасус се сакрива. Цео скрипт ћеш разумети када научиш JavaScript, а
   за сада је довољно само да видиш како отприлике изгледа додавање
   функционалности веб-странама.

Поред скриптова који се извршавају унутар прегледача веба (кажемо на
страни клијента), на вебу се користе и скриптови који се извршавају на
веб-серверу (кажемо на страни сервера). Они се обично пишу у језицима
**PHP**, **C# / ASP.NET**, **Java / JSP**, **Python**, **JavaScript /
Node.js** итд. Ти скриптови обично примају податке са клијента, на
основу њих прибављају податке из базе (или их уписују у базу), након
чега резултате уписују у веб-страницу коју формирају (записану у
формату HTML) и шаљу назад клијенту да је прикаже.

   
Скриптови у играма
..................
   
Још један пример примене скриптова су скриптови у рачунарским играма,
који се користе за њихово прилагођавање. Игре су обично испрограмиране
у језицима попут језика C или C++, који служе за програмирање веома
ефикасних апликација. Неке игре допуштају напредним корисницима да
коришћењем једноставнијих, специјализованих скрипт језика подесе неке
елементе игре (на пример, положај и изглед неких ликова у игри,
понашање непријатељских ликова и слично). Игра написана у ефикасном
језику чита и извршава такве скриптове током свог извршавања. На тај
начин онај ко подешава игру има на располагању изражајност које
програмирање нуди (на пример, сам може да осмисли и опише алгоритам
кретања неких ликова), а са друге стране не мора да има приступ
целокупном изворном коду ире који је веома компликован и написан у
напредном језику. На пример, популарна игра *World of Warcraft* је
имплементирана у језику C++, а корисницима нуди могућност писања
скриптова у језику **Lua** којима могу, на пример, да прилагођавају и
подешавају изглед корисничког интерфејса у овој игри.

.. infonote::

  Размотримо један пример скрипта написаног у језику Lua за игру
  World of Warcraft.
   
  .. code-block:: lua
   
       function castFire(pUnit, Event)
          if pUnit:GetHealthPct() < 50 then
             pUnit:RemoveEvents();
             pUnit:SendChatMessage(12, 0, "Burning Fire!")
             pUnit:FullCastSpellOnTarget(30926)
          end
       end
     
       function OnCombat(pUnit, Event)
           pUnit:RegisterEvent("castFire")
       end
     
       RegisterpUnitEvent(ID, 1, "OnCombat")

  Играчу са идентификатором 1 се придружује додатно понашање -- када
  му проценат здравља падне испод 50%, он аутоматски баца ватрену чин
  (и обавештава све о томе поруком "Burnihg Fire!"). Ако те занима да
  разумеш све детаље ове технике, можеш самостално кренути да
  проучаваш језик Lua и његову примену у игри WoW (овде смо га навели
  чисто да добијеш неку основну идеју функционише скриптовање у
  играма).

Командни скриптови
..................
  
Скриптови се често пишу и да олакшају послове администрирања
(одржавања) рачунара. Оперативни системи подржавају извршавање
скриптова написаних у специјализованим скрипт језицима. Краћи
скриптови се могу навести директно у командној линији (до које се
долази када се покрене посебан програм који се у систему Windows
назива Command Prompt, а у системима Linux обично Terminal или
Console). На пример, ако желимо да направимо резервну копију сваке
текстуалне датотеке у командој линији система Windows можемо
употребити наредну команду:

.. code-block:: batch

   for %%i in (*.txt) do (copy "%%i" "%%i.bak")

Исти задатак би се у оперативном систему и његовом командом
интерпретатору **Bash** извршио следећом командом:

.. code-block:: bash

   for f in *.txt; do cp $f $f.bak; done

Дужи скриптови се често смештају у такозване **беч датотеке**
(енгл. *batch files*). У систему Windows оне имају екстензију
``bat``. У систему Линукс се оне обично чувају у датотекама које имају
екстензију ``sh`` (енгл. *shell scripts*). 


.. infonote::

   Име **беч обрада** потиче још из доба рачунара који су се
   програмирали бушеним картицама. Рачунару се тада кутија
   (енгл. batch) картица које су се обрађивале једна за другом. Тако
   беч датотека садржи команде које се извршавају серијски, једна за
   другом. 

.. infonote::

   Размотримо беч датотеку која прави резервну копију датотеке чије се
   име задаје као аргумент приликом покретања беч датотеке. Резервна
   копија се креира у директоријуму ``backup`` а на име датотеке се
   додаје текући датум. На пример, ``beleske.txt`` се назива
   ``beleske_23-09-2023.txt``). Наравно, у овом тренутку не можете
   разумети све детаље наведених скриптова, али на основу наведених
   коментара у коду можете стећи неку идеју како изгледају командни
   скриптови.

   .. code-block:: batch

      @echo off
       
      rem Ime datoteke se zadaje kao argument komandne linije
      set "sourceFile=%1"
      set "backupFolder=backup"
       
      rem Određujemo sistemski datum i postavljamo odgovarajuće promenljive
      for /f "tokens=2-4 delims=/ " %%a in ('date /t') do (
          set "year=%%c"
          set "month=%%a"
          set "day=%%b"
      )
       
      rem Kreiramo nisku koja sadrži tekući datum
      set "date=%year%%month%%day%"
   
      rem Izdvajamo naziv datoteke i ekstenziju
      for %%i in ("%sourceFile%") do (
          set "filename=%%~ni"
          set "extension=%%~xi"
      )
      
      rem Kreiramo ceo naziv rezervne kopije
      set "backupFile=%sourceFile:.=_%date%.%extension%"
       
      rem Ako ne postoji, kreiramo direktorijum u koji ćemo smestiti rezervnu kopiju
      if not exist "%backupFolder%" (
          mkdir "%backupFolder%"
      )
       
      rem Kopiramo originalnu datoteku
      copy "%sourceFile%" "%backupFolder%\%backupFile%"
   
      rem Ispisujemo poruku da je napravljena rezervna kopija
      echo Backed up "%sourceFile%" to "%backupFolder%\%backupFile%"

   Наведимо и еквивалентан скрипт написан за командни интерпретатор
   Bash који се користи у систему Linux.
      
   ..  code-block:: bash

     #!/bin/bash
      
     # Ime datoteke se zadaje kao argument komandne linije
     sourceFile="$1"
     backupFolder="backup"

     # Ako nije navedeno ime datoteke, štampa se poruka o grešci
     if [ -z "$sourceFile" ]; then
         echo "Usage: $0 source_file"
         exit 1
     fi
      
     # Kreiramo nisku koja sadrži tekući datum
     timestamp=$(date +"%Y%m%d")
      
     # Izdvajamo naziv datoteke i ekstenziju
     filename=$(basename -- "$sourceFile")
     extension="${filename##*.}"
     filename="${filename%.*}"
      
     # Kreiramo ceo naziv rezervne kopije
     backupFile="${filename}_${timestamp}.${extension}"
      
     # Ako ne postoji, kreiramo direktorijum u koji ćemo smestiti rezervnu kopiju
     if [ ! -d "$backupFolder" ]; then
         mkdir "$backupFolder"
     fi
      
     # Kopiramo originalnu datoteku
     cp "$sourceFile" "$backupFolder/$backupFile"

     # Ispisujemo poruku da je napravljena rezervna kopija
     echo "Backed up \"$sourceFile\" to \"$backupFolder/$backupFile\""
                    
Још један чест избор за писање командних скриптова данас је језик
**Python**. У његовој стандардној библиотеци присутна је подршка за
све основне операције над датотекама и директоријумима. Важна предност
коришења језика Python је то што се тако написани скриптови могу
користити на разним оперативним системима (кажемо да су преносиви, за
разлику од скриптова написаних у командном интерпретатору неког
специфичног оперативног система).

.. infonote::

   Претходни скрипт се може написати у језику Python на следећи начин.

   .. code-block:: python

      import os
      import sys
      import shutil
      from datetime import datetime
       
      # Ime datoteke se zadaje kao argument komandne linije
      # Ako argument nije naveden, prijavljuje se greška
      if len(sys.argv) != 2:
          print("Usage: python backup_script.py source_file")
          sys.exit(1)
       
      source_file = sys.argv[1]
      backup_folder = "backup"

      # Ako originalna datoteka ne postoji, prijavljuje se greška
      if not os.path.exists(source_file):
          print(f"Error: Source file '{source_file}' does not exist.")
          sys.exit(1)
       
      # Kreiramo nisku koja sadrži tekući datum
      timestamp = datetime.now().strftime("%Y%m%d")
       
      # Izdvajamo naziv datoteke i ekstenziju
      filename, extension = os.path.splitext(os.path.basename(source_file))
       
      # Konstruišemo naziv rezervne kopije
      backup_file = f"{filename}.{timestamp}{extension}"
       
      # Ako ne postoji, kreiramo direktorijum u koji ćemo smestiti rezervnu kopiju
      if not os.path.exists(backup_folder):
          os.makedirs(backup_folder)
       
      # Kopiramo originalnu datoteku
      destination_file = os.path.join(backup_folder, backup_file)
      shutil.copy2(source_file, destination_file)

      # Ispisujemo poruku da je napravljena rezervna kopija
      print(f"Backed up '{source_file}' to '{destination_file}'")


Задаци за самостални рад
------------------------

1. Проучи историјат језика JavaScript. Када је и како настао, за шта
   се у почетку користио, а како се сада све користи. Какав је однос
   језика JavaScript са језиком Java?

1. Проучи шта је PowerShell, која компанија га је развила, чему служи
   и на којим све оперативним системима може да се користи.

1. Пронађи на интернету неколико примера рачунарских игара које
   допуштају прилагођавање коришћењем скриптова. Ако имаш неку од тих
   игара, покушај да је прилегодиш писањем неког једноставног скрипта.

1. Покушај да напишеш командни скрипт за твој оперативни систем који
   пребројава колико слика у формату ``jpg`` и ``png`` се налази у
   задатом директоријуму.
   
1. Покушај да напишеш командни скрипт за твој оперативни систем који
   уклања све помоћне датотеке (са екстензојом ``bak``, ``log`` и
   слично) у текућем директоријуму и свим његовим поддиректоријумима.
