PROLOG - задаци за самостални рад
---------------------------------

.. questionnote::

   Дефинисати PROLOG предикат ``same_length`` који проверава да ли су
   две дате листе исте димензије.

.. reveal:: prologresenje1
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење

   .. code-block:: prolog

      same_length([], []).
      same_length([_|T1], [_|T2]) :- same_length(T1, T2).                   

.. questionnote::

   Дефинисати PROLOG предикат ``sum`` који одређује збир свих
   елемената дате листе.

.. reveal:: prologresenje2
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење

   .. code-block:: prolog

      sum([], 0).
      sum([H|T], S) :- sum(T, ST), S is H + ST.
   

.. questionnote::

   Дефинисати PROLOG предикат ``min_list`` који одређује најамњи
   елемент дате непразне листе.

.. reveal:: prologresenje3
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење

   .. code-block:: prolog

      min(X, Y, X) :- X =< Y.
      min(X, Y, Y) := X > Y.
      min_list([X], X).
      min_list([H|T], M) :- min_list(T, MT), min(H, MT, M).
   
.. questionnote::

   Дефинисати PROLOG предикат ``digits`` који одређује листу цифара
   датог броја.

.. reveal:: prologresenje4
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење

   .. code-block:: prolog

      digits(0, []).
      digits(N, [D|T]) :- N > 0, D is N mod 10, N1 is N div 10, digits(N1, T).
   

.. questionnote::

   Дефинисати предикат ``odd_elements`` који издваја све непарне
   елементе дате листе бројева.

.. reveal:: prologresenje5
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење

   .. code-block:: prolog

      odd_elements([], []).
      odd_elements([H|T], [H|T1]) :- H1 is H mod 2, H1 == 1, odd_elements(T, T1), !.
      odd_elements([_|T], T1) :- odd_elements(T, T1).
   

.. questionnote::

   Дефинисати предикат ``squares`` који одређује листу квадрата свих
   елемената дате листе.

.. reveal:: prologresenje6
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење

   .. code-block:: prolog

      squares([], []).
      squares([H|T], [H1|T1]) :- H1 is H*H, squares(T, T1).

   
.. questionnote::

   Дефинисати PROLOG предикат који одређује збир квадрата непарних
   цифара датог броја.
   
.. reveal:: prologresenje7
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење

   У решењу употребити све функције из претходних задатака. 
               
   .. code-block:: prolog

      sum_squares_odd_digits(N, M) :-
          digits(N, D), odd_elements(D, DO), squares(DO, DOS), sum(DOS, M).
   

   Решење без коришћења помоћних предиката би било веома компликовано.

.. questionnote::

   Дефинисати PROLOG предикат ``insert`` који умеће елемент на његово
   место у сортираној листи. Дефинисати предикат ``insertion_sort``
   који сортира листу алгоритмом сортирања уметањем.
   
   
.. reveal:: prologresenje8
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење

   .. code-block:: prolog

      insert(X, [], [X]).
      insert(X, [H|T], [X,H|T]) :- X < H, !.
      insert(X, [H|T], [H|T1]) :- insert(X, T, T1).

      insertion_sort([], []).
      insertion_sort([H|T], S) :- insertion_sort(T, ST), insert(H, ST, S).

.. questionnote::

   Дефинисати PROLOG предикат ``permutations`` који набраја све
   пермутације елемената дате листе.

.. reveal:: prologresenje9
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење

   Једно решење се заснива на идеји да на све могуће начине из листе
   извадимо неки елемент тј. да листу раздвојимо на тај елемент и
   листу свих осталих елемената. Дефинисаћемо предикат који то ради.
   Пермутације добијамо тако што на све могуће начине одаберемо први
   елемент у пермутацији, а затим рекурзивно пермутујемо остале
   елементе листе.
               
   .. code-block:: prolog

      permutations([], []).
      permutations(L, [H|T]) :- select(H, L, L1), permutations(L1, T).

      select(H, [H|T], T).
      select(X, [H|T], [H|T1]) :-  select(X, T, T1).

   Друго могуће решење се заснива на убацивању датог елемента на
   произвољну позицију у листи, што реализујемо помоћу предиката
   ``interleave``. Каква год да је листа, елемент се може убацити на
   њен почетак. Ако је листа непразна, онда може бити убачен и иза
   првог елемента.

   Пермутације празне листе чини једино празна листа. Ако је листа
   непразна, онда можемо рекурзивно да пермутујемо њен реп, а главу да
   убацимо на произвољно место у листи.

   .. code-block:: prolog

      interleave(X, L,[X|L]).
      interleave(X, [H|T], [H|T1]) :- interleave(X, T, T1).

      permutations([], []).
      permutations([H|T], P) :- permutations(T, T1), interleave(H, T1, P).

.. questionnote::

   Дефинисати PROLOG предикат који проверава да ли је листа ``P``
   подскуп листе ``S`` (обе су сортиране и имају све различите
   елементе. Предикат треба да може да се употреби и за генерисање
   свих подскупова дате листе.

.. reveal:: prologresenje10
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење


   .. code-block:: prolog
                   
      podskup([], []).
      podskup([X|Xs], [Y|Ys]) :- X = Y, podskup(Xs, Ys).
      podskup(Xs, [_|Ys]) :- podskup(Xs, Ys)

      
.. questionnote::

   Напиши PROLOG предикат који решава следећу загонетку:

   Неколико пријатеља је гласало које би градове желели да посете.
   
   1. Гласали су за Каиро, Лондон, Пекинг, Москву, Бомбај, Најроби и Џакарту.
      
   2. Један град је добио 4 гласа, два града по 2 гласа, два града по 1 глас
      и два града нису добили ниједан глас.

   3. Каиро и Пекинг су добили различит број гласова.
      
   4. Москва је добила или најмање или највише гласова од свих градова.
      
   5. Каиро је добио више гласова од Џакарте.
      
   6. Гледајући листу из тачке 1, тачно два пута се догодило да је
      град са два гласа досао непосредно иза града без гласова.
     
   7. Џакарта је добила или један глас мање од Лондона или један
      глас мање од Пекинга.

  
.. reveal:: prologresenje11
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење


   Решење се може представити листом бројева, која мора бити
   пермутација листе ``[4, 2, 2, 1, 1, 0, 0]``. Сви услови осим оног
   под бројем 6 се онда могу веома једноставно кодирати. За услов из
   тачке 6 дефинишемо помоћу функцију која проверава колико се пута
   двочлана подлиста јавља унутар дате листе.
      
   .. code-block:: prolog
                   
      brojPojavljivanjaPara([], _, 0).
      brojPojavljivanjaPara([X1,X2|XS], [X1,X2], N) :-
          brojPojavljivanjaPara(XS, [X1, X2], N1), N is N1 + 1, !.
      brojPojavljivanjaPara([_|XS], [X1,X2], N) :-
          brojPojavljivanjaPara(XS, [X1, X2], N).
       
      glasovi(Gradovi) :-
          Gradovi = [Kairo, London, Peking, Moskva, Bombaj, Najrobi, Dzakarta],
          permutation(Gradovi, [4, 2, 2, 1, 1, 0, 0]),
          (Kairo < Peking; Kairo > Peking),
          (Moskva = 0 ; Moskva = 4),
          Kairo > Dzakarta,
          brojPojavljivanjaPara(Gradovi, [0, 2], 2),
          (Dzakarta is (London-1); Dzakarta is (Peking-1)).
          
.. questionnote::

    Напиши PROLOG решење следеће логичке загонетке, дате у облику
    песме.
   
    ::
       
       1  Four couples in all
          Attended a costume ball.
       2  The lady dressed as a cat
          Arrived with her husband Matt.
       3  Two couples were already there,
          One man dressed like a bear.
       4  First to arrive wasn't Vince,
          But he got there before the Prince.
       5  The witch (not Sue) is married to Chuck,
          Who was dressed as Donald Duck.
       6  Mary came in after Lou, 
          Both were there before Sue.
       7  The Gipsy arrived before Ann,
          Neither is wed to Batman.
       8  If Snow White arrived after Tess,
          Then how was each couple dressed?
          
