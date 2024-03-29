Логичке загонетке
-----------------

Веома лепа илустрација моћи језика Prolog у односу на друге програмске
језике је кроз решавање логичких загонетки. Обично су програми само
прецизно кодирање услова загонетки, а систем онда самостално проналази
решење, што је доста ефикасније него код других програмских језика у
којима програмер мора да програмира поступак одређивања решења. У
зависности од загонетке која се решава и начина решавања, програми
могу бити мање или више ефикасни. У наредним решењима нећемо обраћати
много пажње на ефикасност решења, већ пре свега на једноставност
њиховог програмирања. Размотримо следећих неколико примера.

.. questionnote::

   Неколико пријатеља је гласало које би градове желели да посете.
    
   1. Гласали су за Каиро, Лондон, Пекинг, Москву, Бомбај, Најроби и
      Џакарту.
   2. Један град је добио 4 гласа, два града по 2 гласа, два града по 1
      глас и два града нису добили ниједан глас.
   3. Каиро и Пекинг су добили различит број гласова.
   4. Москва је добила или најмање или највише гласова од свих градова.
   5. Каиро је добио више гласова од Џакарте.
   6. Гледајући листу из тачке 1, тачно два пута се догодило да је град
      са два гласа дошао непосредно иза града са једним гласом.
   7. Џакарта је добила или један глас мање од Лондона или један
      глас мање од Пекинга.

Направићемо листу променљивих које одговарају градовима. Вредност
сваке од тих променљивих биће број гласова које је тај град добио. На
основу другог правила знамо да ће листа вредности тих променљивих
садржати вредности ``[4, 2, 2, 1, 1, 0, 0]``, али не знамо у ком
редоследу, тј. знаћемо да је листа вредности променљивих једна од
пермутација ове листе. Можемо употребити библиотечки предикат
``permutation`` који се може употребљавати за набрајање свих
пермутација (покушајте да за вежбу самостално дефинишете такав
предикат). Приступ решењу је, дакле, прилично директан: набрајају се
све пермутације ове листе и за сваку од њих се проверава да ли
задовољава додатне услове. Иако су оваква решења у општем случају
веома неефикасна, јер број пермутација брзо расте, у овом конкретном
задатку ефикасност је задовољавајућа јер пермутација седмочлане листе
има тек око 5000. Сваки даљи услов, осим услова 6, веома се директно
кодира. Што се тиче услова 6, дефинишемо помоћни предикат који броји
појављивања пара бројева у листи.
 
.. code-block:: prolog
                
    brojPojavljivanjaPara([], _, 0).
    brojPojavljivanjaPara([X1,X2|T], [X1,X2], N) :-
       brojPojavljivanjaPara(T, [X1,X2], N1), N is N1 + 1, !.
    brojPojavljivanjaPara([_|T], [X1,X2], N) :-
       brojPojavljivanjaPara(T, [X1,X2], N).
 
    glasovi(Gradovi) :-
      Gradovi = [Kairo,London,Peking,Moskva,Bombaj,Najrobi,Dzakarta],
      permutation(Gradovi, [4, 2, 2, 1, 1, 0, 0]),
      Kairo =\= Peking,
      (Moskva = 0 ; Moskva = 4),
      Kairo > Dzakarta,
      brojPojavljivanjaPara(Gradovi, [0, 2], 2),
      (Dzakarta is (London-1); Dzakarta is (Peking-1)).


Тачно решење ``Gradovi = [4, 0, 2, 0, 2, 1, 1]`` се пријављује
неколико пута, јер функција за проналажење пермутација неколико
пута проналази исту пермутацију (зато што листа садржи дупликате).

.. questionnote::
   
   Пет људи различитих националности живи у пет кућа различитих боја,
   имају пет различитих врста кућних љубимаца, пију пет различитих
   напитака и пуше пет различитих врста цигарета.
   
   1. Норвежанин живи у првој кући.
   2. Млеко се пије у средњој кући.
   3. Енглез живи у црвеној кући.
   4. Шпанац има пса.
   5. У зеленој кући се пије кафа.
   6. Украјинац пије чај.
   7. Власник пужа пуши цигарете „олд голд“.
   8. У жутој кући се пуше цигарете „кулс“.
   9. Зелена кућа је прва десно од куће боје слоноваче.
   10. У кући поред оне у којој живи лисица се пуше цигарете „честер“.
   11. У кући поред оне у којој се пуше цигарете „кулс“ је љубимац коњ.
   12. Плава кућа је поред оне у којој живи Норвежанин.
   13. Власник једне куће пуши цигарете „лаки“ и пије ђус.
   14. Јапанац пуши цигарете „парламент“.

Напиши програм који одређује ко је власник зебре и ко пије воду?

Кључно питање је како представити решење. Веома погодна
репрезентација је у облику листе термова где је сваки терм облика
``kuca(nacionalnost, boja, ljubimac, pice, cigarete)``. Тада можемо
креирати петочлану листу ``Kuce`` и кодирати услове о њеним
члановима. Прва два услова се могу кодирати приликом дефинисања
низа кућа: први елемент је облика ``kuca(norvezanin, _, _, _, _)``,
где анонимне променљиве могу бити унификоване са стварним
вредностима на тим позицијама, а трећи елемент је облика
``kuca(_, _, _, mleko, _)``. О другом, четвртом и петом елементу листе
не знамо ништа, па их можемо представити анонимним променљивама. У услову
9. потребно је да кодирамо „кућа је десно од куће“, а у неколико
услова треба да кодирамо да су куће једна поред друге. За то
дефинишемо два помоћна предиката ``desnoOd`` и ``pored`` (који се лако
кодира помоћу ``desnoOd`` зато што је кућа поред куће ако и само ако
је прва десно од друге или друга десно од прве). Све остале услове
кодирамо коришћењем уграђеног предиката ``member``, који проверава да
ли дати елемент припада листи, али и може да наброји редом чланове
листе. Имајући све ово у виду, сви услови се прилично директно
кодирају (приметимо да услови различитости следе из тога што за сваку
категорију имамо пет различитих константи, након што као последња два
услова додамо информације о томе да власник зебре има зебру, а да онај
ко пије воду пије воду). Покретањем предиката ``zebraZagonetka``
добијамо јединствено решење да Јапанац има зебру, а да Норвежанин пије
воду.

.. code-block:: prolog

  desnoOd(X, Y, [Y, X| _]).
  desnoOd(X, Y, [_|T]) :- desnoOd(X, Y, T).
  pored(X, Y, L) :- desnoOd(X, Y, L) ; desnoOd(Y, X, L).

  zebraZagonetka(VlasnikZebre, PijeVodu) :-
     Kuce = [kuca(norvezanin, _, _, _, _), _, kuca(_, _, _, mleko, _), _, _],
     member(kuca(englez, crvena, _, _, _), Kuce),
     member(kuca(spanac, _, pas, _, _), Kuce),
     member(kuca(_, zelena, _, kafa, _), Kuce),
     member(kuca(ukrajinac, _, _, caj, _), Kuce),
     member(kuca(_, _, puz, _, oldgold), Kuce),
     member(kuca(_, zuta, _, _, kuls), Kuce),
     desnoOd(kuca(_, zelena, _, _, _), kuca(_, slonovaca, _, _, _), Kuce),
     pored(kuca(_, _, _, _, cester), kuca(_, _, lisica, _, _), Kuce),
     pored(kuca(_, _, _, _, kuls), kuca(_, _, konj, _, _), Kuce),
     pored(kuca(norvezanin, _, _, _, _), kuca(_, plava, _, _, _), Kuce),
     member(kuca(_, _, _, djus, laki), Kuce),
     member(kuca(japanac, _, _, _, parlament), Kuce),
     member(kuca(VlasnikZebre, _, zebra, _, _), Kuce),
     member(kuca(PijeVodu, _, _, voda, _), Kuce).


Судоку решавач
..............

У претходним поглављима смо видели неколико решења загонетке Судоку у
различитим програмским језицима и системима. У наставку следе два
правила у језику Prolog која могу да реше Судоку загонетку димензије
:math:`4\times 4`. Табла је представљена као листа листи и Кодирано је
да је свака врста, свака колона и сваки квадрат димензије :math:`2
\times 2` пермутација бројева од 1 до 4. На основу ових ограничења
Prolog може да наброји све исправне Судоку табле. За набрајање
пермутација коришћен је библиотечки предикат ``permutation`` (а раније
смо приказали како се ова функционалност може ручно
кодирати). Приметимо да је бектрекинг претрага уграђена у сам језик и
да, за разлику од имплементација у императивним и функционалним
језицима, она није видљива у програмском коду. Програм је сасвим
декларативан и процедура претраге решења је у потпуности сакривена од
програмера.

.. code-block:: prolog

   permutacija(Xs) :- permutation(Xs, [1, 2, 3, 4]).
    
   sudoku(Xs) :- Xs = [[A11, A12, A13, A14],
                       [A21, A22, A23, A24],
                       [A31, A32, A33, A34],
                       [A41, A42, A43, A44]],
       
       permutacija([A11, A12, A13, A14]),
       permutacija([A21, A22, A23, A24]),
       permutacija([A31, A32, A33, A34]),
       permutacija([A41, A42, A43, A44]),
       
       permutacija([A11, A21, A31, A41]),
       permutacija([A12, A22, A32, A42]),
       permutacija([A13, A23, A33, A43]),
       permutacija([A14, A24, A34, A44]),
    
       permutacija([A11, A12, A21, A22]),
       permutacija([A13, A14, A23, A24]),
       permutacija([A31, A32, A41, A42]),
       permutacija([A33, A34, A43, A44]).
       
Уопштавање овог проблем на таблу веће димензије, нажалост, не даје
програм који је довољно ефикасан.

.. infonote::

   Задатак се ефикасно решава ако се користи проширење језика Prolog
   које подржава тзв. **програмирање ограничења над коначним
   доменима** (енгл. constraint logic programming over finite
   domains). SWI Prolog подржава ово проширење, ако се укључи
   одговарајућа библиотека. Наредно решење је управо и приказано на
   сајту SWI Prologa, као пример употребе ове библиотеке.

   .. code-block:: prolog
                   
      :- use_module(library(clpz)).
      :- use_module(library(lists)).
      
      sudoku(Rows) :-
              length(Rows, 9), maplist(same_length(Rows), Rows),
              append(Rows, Vs), Vs ins 1..9,
              maplist(all_distinct, Rows),
              transpose(Rows, Columns), maplist(all_distinct, Columns),
              Rows = [As,Bs,Cs,Ds,Es,Fs,Gs,Hs,Is],
              blocks(As, Bs, Cs),
              blocks(Ds, Es, Fs),
              blocks(Gs, Hs, Is).
       
      blocks([], [], []).
      blocks([N1,N2,N3|Ns1], [N4,N5,N6|Ns2], [N7,N8,N9|Ns3]) :-
              all_distinct([N1,N2,N3,N4,N5,N6,N7,N8,N9]),
              blocks(Ns1, Ns2, Ns3).
       
      problem(1, [[_,_,_,_,_,_,_,_,_],
                  [_,_,_,_,_,3,_,8,5],
                  [_,_,1,_,2,_,_,_,_],
                  [_,_,_,5,_,7,_,_,_],
                  [_,_,4,_,_,_,1,_,_],
                  [_,9,_,_,_,_,_,_,_],
                  [5,_,_,_,_,_,_,7,3],
                  [_,_,2,_,1,_,_,_,_],
                  [_,_,_,_,4,_,_,_,9]]).                

   Ово решење користи неколико библиотечких предиката.

   - ``length(Rows, 9)`` каже да је решење листа дужине 9
   - Предикат ``maplist`` успева ако се први дати аргумент, који је
     предикат, успешно примењује на све елементе другог датог
     аргумента, који је листа. У овом случају сваки елемент листе
     ``Rows`` мора да има исту дужину као и листа ``Rows`` (предикат
     ``same_length`` проверава да ли су две дате листе исте дужине).
   - Предикатом ``append(Rows, Vs)`` се израчунава листа ``Vs`` која
     се добија надовезивањем свих листи из листе ``Rows``. Након тога
     се оператором ``ins`` који је део библиотеке за програмирање
     ограничења над коначним доменима проглашава да свака променљива у
     тој листи има као домен скуп бројева од 1 до 9.
   - Предикатом ``maplist(all_distinct, Rows)`` се захтева да се свака
     врста састоји од различитих бројева (предикат ``all_distinct``
     који је део поменуте библиотеке то ефикасно проверава).
   - Захтев да су елементи сваке колоне сви различити се поставља на
     сличан начин, тако што се пре тог захтева матрица транспонује
     (мењају се улоге врста и колона) коришћењем библиотечког
     предиката ``transpose``.
   - На крају се манеће услов различитости елемената за сваки квадрат
     димензије :math:`3\times 3`. Већ смо разјаснили улогу сваког
     библиотечког предиката, па покушај самостално да разумеш како тај
     део кода ради.
   
