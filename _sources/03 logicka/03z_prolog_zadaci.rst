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

   .. reveal:: prologresenje8
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
