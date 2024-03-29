Аритметичка израчунавања у језику Prolog
----------------------------------------

xИако је Prolog заснован на математичкој логици и његову основу, као
што смо видели, чини симболичко израчунавање, програмирање се не може
замислити без нумеричког израчунавања, тј. рада са бројевима. Prolog
подржава рад и са целим и са реалним бројевима, али jе често за то
потребно користити посебну подршку.

Кренимо од употребе релацијских оператора. Дефинишимо предикате којима
се одређује агрегатно стање воде.

.. code-block:: prolog

   cvrsto(X) :- X < 0.
   tecno(X) :- X >= 0, X < 100.
   gasovito(X) :- X >= 100.                

Ови предикати су коректно дефинисани и дају исправан резултат за сваку
проверу.

Интересантан је и следећи пример. У бази знања памтимо почетак и крај
владавине неколико краљева из династије Немањића. Затим дефинишемо да
је неко био краљ током дате године, ако је та година унутар интервала
његове владавине.

.. code-block:: prolog

   kralj_od_do(stefan, 1217, 1228).
   kralj_od_do(radoslav, 1228, 1233).
   kralj_od_do(vladislav, 1234, 1243).
   kralj_od_do(uros, 1243, 1276).
   kralj_od_do(dragutin, 1276, 1282).
   kralj(Ime, Godina) :- kralj_od_do(Ime, GodinaOd, GodinaDo),
                         GodinaOd =< Godina, Godina =< GodinaDo.
   
Сада можемо да питамо и ко је био краљ током 1250. године.

.. code-block:: prolog

   ?- kralj(Ime, 1250).

Систем исправно изводи закључак да је једини краљ током те године био
Урош.
                         
Релацијски оператори се, дакле, на први поглед понашају прилично
очекивано. Међутим, Prolog неће успети да нам одговори током којих је
све година Стефан био краљ.


.. code-block:: prolog

   ?- kralj(stefan, Godina).

На овај упит добијамо одговор

::

   Arguments are not sufficiently instantiated
   In:
   [2] 1217 =< _1702
   [1] kralj(stefan,_1756) at  line 7

који нам каже да није могуће да се релацијски оператор примени на
променљиву којој још није одређена вредност (у нашем случају то је
променљива ``Godina``).
   
Размотримо сада следећи пример предиката који користи операцију
сабирања.

.. code-block:: prolog

   zbir(X, Y, Z) :- Z == X + Y.

Ова дефиниција је синтаксички исправна, што значи да Prolog зна нешто
о сабирању и једнакости. Међутим, ако поставимо следећи упит

.. code-block:: prolog

   ?- zbir(3, 5, 8).

неочекивано добијамо неисправан одговор ``false``. И на упит

.. code-block:: prolog

   ?- zbir(3, 5, X).

добијамо одговор ``false``. Међутим, ако поставимо упит


.. code-block:: prolog

   ?- zbir(3, 5, 3 + 5).

добијамо одговор ``true``.

Нешто очигледно није како треба. Покушајмо да променимо дефиницију
предиката ``zbir`` и да уместо оператора ``==`` употребимо оператор
``=``.

.. code-block:: prolog

   zbir(X, Y, Z) :- Z = X + Y.

И ова дефиниција је синтаксички исправна, што значи да Prolog користи
и оператор ``==`` и ``=`` (и видећемо да они означавају различите
ствари). Међутим, ако поставимо следећи упит

.. code-block:: prolog

   ?- zbir(3, 5, 8).

поново добијамо неисправан одговор ``false``. Са друге стране, на упит

.. code-block:: prolog

   ?- zbir(3, 5, X).

сада добијамо одговор ``3+5``, што је делимично тачно (јер, наравно,
очекујемо одговор ``8``). Поново на упит

.. code-block:: prolog

   ?- zbir(3, 5, 3 + 5).

добијамо тачан одговор ``true``.

Шта се заправо овде догађа? Оператор ``==`` је **оператор провере
једнакости** два терма и он враћа вредност тачно ако и само ако су
термови идентични.

- Упит ``?- zbir(3, 5, 8)`` се своди на ``8 == 3+5``. Проверава се да ли
  су терм са леве и десне стране идентични, они то нису и добија се
  одговор ``false``.

- Упит ``?- zbir(3, 5, X)`` се своди на ``X == 3+5``. Проверава се да ли
  су терм са леве и десне стране идентични, они то нису и добија се
  одговор ``false``.

- Упит ``?- zbir(3, 5, 3+5)`` се своди на ``3+5 == 3+5``. Проверава се да
  ли су терм са леве и десне стране идентични, они јесу идентични и
  добија се одговор ``true``.

Оператор ``=`` је **оператор унификације** и он враћа вредност тачно
ако и само ако се термови могу унификовати, тј. ако се променљивама
доделити вредности тако да два терма постану једнака након те доделе.

- Упит ``?- zbir(3, 5, 8)`` се своди на ``8 = 3+5``. Пошто се термови
  не могу унификовати (у њима се не јављају променљиве) добија се
  одговор ``false``.

- Упит ``?- zbir(3, 5, X)`` се своди на ``X = 3+5``. Термови са леве и
  десне стране се могу унификовати тако што се променљивој ``X``
  додели вредност ``3+5``, па упит успева уз резултат ``X=3+5``.

- Упит ``?- zbir(3, 5, 3+5)`` се своди на ``3+5 == 3+5``. Термови са леве
  и десне стране су идентични (па се самим тим могу и унификовати) и
  као резултат се добија ``true``.

Објаснили смо операторе ``==`` и ``=``, али нам ни један од њих не
одговара у потпуности. Да би се извршило сабирање (или било која друга
аритметичка операција), потребно је да се употреби оператор ``is``.
Њиме се проверава да ли се термови са леве и десне стране могу
унификовати, али тек након што се терм са десне стране израчуна.

.. code-block:: prolog

   zbir(X, Y, Z) :- Z is X + Y.
   
- Упит ``?- zbir(3, 5, 8)`` се своди на ``8 is 3+5``. Када се израчуна
  вредност терма са десне стране, добија се вредност 8, па пошто су
  лева и десна страна тада једнаке, добија се исправан резултат
  ``true``.

- Упит ``?- zbir(3, 5, X)`` се своди на ``X is 3+5``. Када се израчуна
  терм са десне стране добијају се термови ``X`` и ``8``, па пошто се
  они могу унификовати тако што се променљивој ``X`` додели вредност
  ``8``, упит успева уз резултат ``X=8``.

- Упит ``?- zbir(3, 5, 3+5)`` се своди на ``3+5 is 3+5``. Када се
  израчуна вредност терма са десне стране, добијају се термови ``3+5``
  и ``8``, који се не могу унификовати и добија се погрешан резултат
  ``false``.

Дакле, ако употребимо оператор ``is``, добијамо исправну могућност
израчунавања вредности израза (у том светлу најзначајнији нам је упит
``zbir(3, 5, X)``), при чему и провера израчунате вредности ради
исправно (упит ``zbir(3, 5, 8)`` коректно ради).

Међутим, важно је нагласити да се из ове релације не могу издвојити
друге функције. На пример, упит ``?- zbir(X, 5, 8)`` даје одговор
``no``. Решавање једначина, дакле, није могуће.

Оператори поређења на једнакост ``=:=`` и различитост ``=\=`` такође
врше израчунавање термова пре поређења.

.. infonote::

   Када год употребљавате аритметичке операторе, морате употребити и
   оператор ``is``, ``=:=`` или ``=\=`` којим ћете натерати систем да
   их примени, тј. да изврши потребна израчунавања!

Релацијски оператори су описани у следећој табели.
   
+-------------+------------------------------------------------------------+
| Оператор    | Опис                                                       |
+=============+============================================================+
| ``=``       | Унификује два терма                                        |
+-------------+------------------------------------------------------------+
| ``\=``      | Негација унификације                                       |
+-------------+------------------------------------------------------------+
| ``==``      | Једнакост два терма                                        |
+-------------+------------------------------------------------------------+
| ``=:=``     | Једнакост израчунатих вредности два терма                  |
+-------------+------------------------------------------------------------+
| ``=\=``     | Негација једнакости                                        |
+-------------+------------------------------------------------------------+
| ``=<``      | Мање од или једнако                                        |
+-------------+------------------------------------------------------------+
| ``<``       | Мање од                                                    |
+-------------+------------------------------------------------------------+
| ``>=``      | Веће од или једнако                                        |
+-------------+------------------------------------------------------------+
| ``>``       | Веће од                                                    |
+-------------+------------------------------------------------------------+

Аритметички оператори су описани у следећој табели.

+-------------+--------------------------------------------------+
| Оператор    | Опис                                             |
+=============+==================================================+
| ``+``       | Сабира два броја.                                |
+-------------+--------------------------------------------------+
| ``-``       | Одузима други број од првог.                     |
+-------------+--------------------------------------------------+
| ``*``       | Множи два броја.                                 |
+-------------+--------------------------------------------------+
| ``/``       | Дели први број са другим.                        |
+-------------+--------------------------------------------------+
| ``//``      | Целобројно дељење (добија целобројни резултат).  |
+-------------+--------------------------------------------------+
| ``mod``     | Остатак при дељењу (добија остатак од дељења).   |
+-------------+--------------------------------------------------+
| ``**``      | Степеновање (први број се степенује другим).     |
+-------------+--------------------------------------------------+

.. questionnote::

   Дефинисати предикат који израчунава степен броја (изложилац је
   ненегативан цео број).


Основна идеја је да пратимо рекурзивну дефиницију која је у језику
Haskell била изражена на следећи начин:

.. code-block:: haskell

   stepen x 0 = 1
   stepen x n = x * stepen x (n - 1)

Уместо функције у језику Prolog дефинишемо предикат, тј. релацију.
Поново имамо два случаја (излаз из рекурзије и рекурзивни корак).
      
.. code-block:: prolog
   
   stepen(X, 0, 1).
   stepen(X, N, S) :- N > 0, N1 is N-1, stepen(X, N1, S1), S is X * S1.

Пошто се у првом правилу вредност променљиве ``X`` не користи,
добијамо упозорење ``Singleton variable X``. Да би се оно избегло,
уместо назива ``X`` можемо употребити анонимну променљиву која се
обележава подвлаком.

.. code-block:: prolog

   stepen(_, 0, 1).
   
Прво правило можемо читати као:

- нулти степен било ког броја је 1*

Друго правило се може протумачити као:

- ако је ``N`` позитиван, ако је ``N1`` једнак вредности броја ``N``
  након што се она умањи за 1, ако је ``S1`` вредност степена ``X`` на
  ``N1`` и ако је ``S`` једнака вредности која се добије када се
  израчуна производ броја ``X`` и те вредности ``S1``, тада је ``S``
  вредност степена ``X`` на ``N``.

Нагласимо да је потребно употребити оператор ``is`` да би се број
``N`` умањио за 1 као и да би се резултат рекурзивног позива ``S1``
помножио са ``X``. Ако не бисмо у другом правилу навели услов ``N >
0``, тада би се прво пријавила исправно израчуната вредност степена,
али би се приликом тражења даљих решења запало у бесконачну рекурзију
јер не би било услова који би спречио да се друго правило примењује на
``N=0`` а затим и на негативне вредности променљиве ``N``.
      
Можемо дефинисати и ефикаснију имплементацију степеновања.

.. code-block:: prolog

   stepen(X, 0, 1).
   stepen(X, N, S) :- N > 0, N mod 2 =:= 0,
                      N1 is N // 2, X2 is X * X, stepen(X2, N1, S).
   stepen(X, N, S) :- N > 0, N mod 2 =\= 0,
                      N1 is N-1, stepen(X, N1, S1), S is S1 * X.


Сечење (додуше црвено) нам може помоћи да поједноставимо неке од
претходних дефиниција и да избегнемо експлицитно навођење додатних
услова. На пример, дефиниција степеновања се упрошћава.

.. code-block:: prolog

   stepen(_, 0, 1) :- !.
   stepen(X, N, S) :- N mod 2 =:= 0,
                      N1 is N // 2, X2 is X * X, stepen(X2, N1, S), !.
   stepen(X, N, S) :- N1 is N-1, stepen(X, N1, S1), S is S1 * X.

   
.. questionnote::

   Дефинисати предикат који Еуклидовим алгоритмом израчунава НЗД два
   дата природна броја.

.. code-block:: prolog

    nzd(A, 0, A).
    nzd(A, B, N) :- B > 0, M is A mod B, nzd(B, M, N).


Дефинишимо и функцију којом се проверава да ли је дати број прост.
Број је прост ако је већи од 1 и ако нема ниједан прост фактор између
два и свог корена. У дефиницији ћемо зато користити негацију тј. израз
``not(faktor(N, 2))`` који ће успети ако не успе предикат ``faktor(N,
2)``. Овај предикат рекурзивно претражује све факторе од 2 до корена
из ``N``. Ако је ``N`` дељив текућим кандидатом ``X``, предикат
``faktor(N, X)`` успева. Други начин да тај предикат успе је да је
``X`` мањи од корена из ``N``, а да предикат успе за вредност
``X+1``. Дакле, предикат ``faktor(N, X)`` успева ако и само ако број
``N`` има неки прост фактор између вредности ``X`` и корен из ``N``.
Приметимо да смо код провере дељивости употребили оператор ``=:=``,
којим се постиже да се пре поређења обе вредности израчунају. Обратимо
пажњу и на то да број 2 мора да се третира као специјални случај (јер
се провера дељивости врши увек за фактор 2).

.. code-block:: prolog

    prost(2).
    prost(N) :- N > 1, not(faktor(N, 2)).

    faktor(N, X) :- N mod X =:= 0.
    faktor(N, X) :- X*X < N, X1 is X + 1, faktor(N, X1).

