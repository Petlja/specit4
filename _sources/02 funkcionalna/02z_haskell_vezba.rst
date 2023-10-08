Задаци за вежбу
---------------

.. questionnote::

   Дефинисати рекурзивну функцију којом се одређује највећи елемент
   непразне листе.

.. reveal:: haskellresenje1
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење
               
   .. code-block:: haskell
    
      my_maximum :: Ord a => [a] -> a
      my_maximum [x] = x
      my_maximum (x:xs) = max x (my_maximum xs)

   
.. questionnote::

   Дефинисати рекурзивну функцију којом се издвајају сви позитивни
   елементи дате листе. Упореди ову функцију са функцијом заснованом
   на функционалу ``filter``.

.. reveal:: haskellresenje
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење
   
   .. code-block:: haskell
    
      positive :: Num a => [a] -> [a]
      positive [] = []
      positive (x:xs) = (if x > 0 then x : positive xs else positive xs)
    
   Решење помоћу ``filter`` је неупоредиво јасније и једноставније.
    
   .. code-block:: haskell
    
      positive :: Num a => [a] -> [a]
      positive = filter (>0)

.. questionnote::

   Дефинисати рекурзивну функцију којом се израчунавају корени свих
   елемената дате листе реалних бројева. Упореди ову функцију са
   функцијом заснованом на функционалу ``map``.

.. reveal:: haskellresenje3
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење
   
   .. code-block:: haskell
    
      square_roots :: [Double] -> [Double]
      square_roots [] = []
      square_roots (x:xs) = sqrt x : square_roots xs
    
   Решење помоћу ``map`` је неупоредиво јасније и једноставније.
   
   .. code-block:: haskell
    
      square_roots :: [Double] -> [Double]
      square_roots = map sqrt
    

.. questionnote::

   Рекурзивно дефинисати функционале ``my_map`` и ``my_filter`` који
   одговарају библиотечким функционалима ``map`` и ``filter``.

.. reveal:: haskellresenje4
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење
   
   .. code-block:: haskell
    
      my_map :: (a -> b) -> [a] -> [b]
      my_map f [] = []
      my_map f (x : xs) = (f x) : my_map f xs
    
      my_filter :: (a -> Bool) -> [a] -> [a]
      my_filter P [] = []
      my_filter P (x : xs) = (if P x then x : my_filter P xs else my_filter P xs)
      
.. questionnote::

   Применом функције ``fold`` дефинисати функцију која надовезује све
   листе које су елементи дате листе (овај ефекат има библиотечка
   функција ``concat``). На пример, ``concat [[1, 2], [3, 4]] = [1, 2, 3, 4]``.

.. reveal:: haskellresenje5
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење
   
   До решења се једноставно долази ако се примети да се резултат добија
   тако што се резултат иницијализује на празну листу, затим се обрађује
   једна по једна листа из дате листе и у саком кораку се резултат
   ажурира тако што се текућа листа надовеже на почетак резултата.

   .. code-block:: haskell
                         
      my_concat :: [[a]] -> [a]
      my_concat = foldr (++) []

.. questionnote::

   За свако возило на ауто-плацу познат је произвођач, година
   производње и цена. Дефинсати функцију која израчунава просечну цену
   возила произведених 2020. године и касније.

.. reveal:: haskellresenje6
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење
   
   .. code-block:: haskell
    
      data Vehicle = Vehicle
        {  brand :: String,
           year :: Int,
           price :: Double   
        }
       
      vehicles :: [Vehicle]
      vehicles = [
         Vehicle { brand="Toyota", year=2020, price=18400 },
         Vehicle { brand="Ford", year=2019, price=17300 },
         Vehicle { brand="Mazda", year=2007, price=12200 },
         Vehicle { brand="BMW", year=2022, price=24000 }]
       
      averagePriceAfter2020 :: Double
      averagePriceAfter2020 =
        average $ map price $ filter (\vehicle -> year vehicle >= 2020) vehicles
        where
          average :: [Double] -> Double
          average xs = sum xs / fromIntegral (length xs)
      
.. questionnote::

   Дефинисати функцију која одређује листу простих чинилаца датог броја
   :math:`n`.

.. reveal:: haskellresenje7
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење
         
   Користимо уобичајени алгоритам факторизације бројева који је обрађен у
   другом разреду.
    
   .. code-block:: csharp
    
      List<ulong> PrimeFactors(ulong n)
      {
         List<ulong> factors = new List<ulong>();
         ulong d = 2;
         while (d * d <= n)
         {
             if (n % d == 0)
             {
                 factors.Add(d);
                 n /= d;
             } else
               d++;
         }
         if (n > 1)
            factors.Add(n);
         return factors;
      }
    
   Исти се алгоритам лако изражава рекурзивно и имплементира у језику Haskell:
      
   .. code-block:: haskell
    
      primeFactors :: Integer -> [Integer]
      primeFactors n = factorize n 2
        where
           factorize k d
              | d * d > k        = [k | k > 1]
              | k `mod` d == 0   = d : factorize (k `div` d) d
              | otherwise        = factorize k (d + 1)
    
   Прикажимо и извршавање овог алгоритма на једном примеру:
    
   ::           
    
     primeFactors 168 =
     factorize 168 2 =
     2 : factorize 84 2 =
     2 : 2 : factorize 42 2 =
     2 : 2 : 2 : factorize 21 2 =
     2 : 2 : 2 : factorize 21 3 =
     2 : 2 : 2 : 3 : factorize 7 3 =
     2 : 2 : 2 : 3 : 7 =
     [2, 2, 2, 3, 7]

.. questionnote::

   Дефинисати функцију која одређује вредност израза датог у
   постфиксној нотацији. На пример, вредност израза ``3 4 + 5 *``
   је 35. У имплементацији је могуће користити функцију ``words`` која
   разбија ниску на подниске раздвојене размацима и функцију ``read``
   која претвара ниску у број.

.. reveal:: haskellresenje8
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење
   
   Основни алгоритам је да се крене од празног стека, да се пролази кроз
   листу токена (бројева или ознака операција) добијених разбијањем
   израза и да се у сваком кораку стек ажурира имајући у виду тренутни
   токен. Ако је тренутни токен број, тада се његова бројна вредност
   додаје на стек, а ако је оператор, тада се скидају две вредности са
   врха стека, на њих се примењује операција и резултат се додаје на
   почетак стека. Резултат се на крају налази на врху стека (под
   претпоставком да је израз исправн, то ће бити и једини елемент на
   стеку).
    
   Прво треба да буде примењена функција ``words``. Након тога се врши
   обрада једног по једног елемента листе слева надесно, ажурирајући при
   том стек. Јасно је да је у питању операција ``foldl``, при чему морамо
   да дефинишемо помоћну функцију ``updateStack`` која врши ажурирање
   стека.  Њена дефиниција је једноставна (анализирамо случајеве и
   користимо поклапање шаблона). При том је важно напоменути да ћемо стек
   имплементирати уз помоћ листе, где ће врх стека бити на почетку листе
   (да бисмо ефикасно могли скидати елементе са врха стека и додавати их
   на стек). На крају издвајамо елемент са врха стека (функцијом
   ``head``, јер је у питању листа).
      
   .. code-block:: haskell
      
      rpn :: [Char] -> Integer
      rpn = head . foldl updateStack [] . words
        where
          updateStack (x : y : xs) "+" = (x + y) : xs
          updateStack (x : y : xs) "*" = (x * y) : xs
          updateStack          xs  num = read num : xs
     
.. questionnote::

   Дефинисати рекурзивну функцију која одређује све префиксе дате листе.

.. reveal:: haskellresenje9
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење
   
   Празна листа је једини префикс празне листе. Ако одредимо све префиксе
   репа листе, тада све непразне префиксе листе можемо добити њиховим
   проширивањем тако што се глава листе дода на почетак. Поред њих, не
   треба да заборавимо и на празан префикс. На пример, ако имамо листу
   ``[1, 2, 3]``, префикси репа листе (рекурзивно одређени) су
   ``[[], [2], [2, 3]]``. Дописивањем јединице на сваки од њих добијамо
   ``[[1], [1, 2], [1, 2, 3]]``. Додавањем празне листе на почетак, добијамо
   листу ``[[], [1], [1, 2], [1, 2, 3]]`` која садржи све
   префиксе полазне листе. Дописивање главе на почетак сваке листе можемо
   лако остварити применом функције ``map``.
    
   .. code-block:: haskell
    
      my_prefixes :: [a] -> [[a]]
      my_prefixes [] = [[]]
      my_prefixes (x:xs) = [] : map (x:) (my_prefixes xs)

.. questionnote::

   Дефинисати функцију која генерише листу која садржи све варијације
   са понављањем дужине n састављене од елемената дате листе. На пример,

   .. code-block:: haskell
                
      Prelude> variations [1, 2, 3] 2
      [[1,1],[1,2],[1,3],[2,1],[2,2],[2,3],[3,1],[3,2],[3,3]]

.. reveal:: haskellresenje10
   :showtitle: Прикажи решење
   :hidetitle: Сакриј решење

   Задатак решавамо рекурзијом по дужини варијације. Претпоставимо да смо
   рекурзивно направили све варијације дужине :math:`n-1`. Сваку од њих
   треба на све могуће начине проширити елементима дате листе. То се
   најједноставније може изразити компрехенсијом. Излаз из рекурзије су
   варијације дужине 0. Тај случај је пипав јер резултат није празна
   листа, већ листа која садржи празну листу (увек постоји тачно једна
   варијација дужине 0 и она је празна).
         
   .. code-block:: haskell
    
      variations xs 0 = [[]]
      variations xs n = let vs = variations xs (n-1)
                         in [x:v | x <- xs, v <- vs]
                         
   .. questionnote::
    
      Дефинисати рекурзивну функцију која уз помоћ функције ``dropWhile``
      уклања све узастопне дупликате из листе. На пример, за листу
      ``[1, 1, 2, 2, 1, 1, 1]`` треба да се добије резултат ``[1, 2, 1]``.
    
    
   Уклањањем дупликата из празне листе добија се празна листа. Ако је
   листа непразна њен први елемент (глава) се задржава, уклањају се сва
   његова појављивања са почетка репа (за то се може употребити функција
   ``dropWhile``), након чега се рекурзивно уклањају сви дупликати из
   репа.
      
   .. code-block:: haskell
        
      remConsecutiveDups :: Eq a => [a] -> [a]
      remConsecutiveDups [] = []
      remConsecutiveDups (x:xs) = x : remConsecutiveDups (dropWhile (== x) xs)
                      
