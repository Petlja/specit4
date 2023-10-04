Примена аутоматских доказивача теорема
--------------------------------------

Један од најуспешнијих аутоматских доказивача теорема је *Vampire*
(https://vprover.github.io/). Преузми га на свој рачунар. Улаз
доказивача се обично задаје у формату TPTP. Прикажимо како можемо
доказати коректност претходног закључка применом аутоматског
доказивача. Две претпоставке задајемо као аксиоме. Формуле логике
првог реда се у формату TPTP означавају са `fof`, након чега се задаје
име формуле, статус (са `axiom` обележавамо претпоставке, а са
`conjecture` закључке) и на крају сама формула. Приметимо да се
импликација обележава са `=>`, а универзални квантификатор sa `!`.

::

   fof(premisa1, axiom, ![X]: (grk(X) => covek(X))).
   fof(premisa2, axiom, ![X]: (covek(X) => smrtan(X))).
   fof(zakljucak, conjecture, ![X]: (grk(X) => smrtan(X))).

Ако ово сачувамо у датотеку `syllogysm1.tptp` и затим покренемо
*Vampire* из командне линије, командом `vampire syllogysm1.tptp`
добијамо поруку:

::

   % Refutation found. Thanks to Tanya!

Ово значи да је формула успешно доказана.

.. questionnote::

   Коришћењем аутоматског доказивача оправдајте и следеће Аристотелове
   силогизме.

   - Ниједан рептил нема крзно.
   - Све змије су рептили.
   - Дакле, ниједна змија нема крзно.

   ::

      fof(prem1, axiom, ~(?[X]: (reptil(X) & krzno(X)))).
      fof(prem2, axiom, ![X]: (zmija(X) => reptil(X))).
      fof(concl, conjecture, ~(?[X]: (zmija(X) & krzno(X)))).
     
   - Сви зечеви имају крзно.
   - Неки љубимци су зечеви.
   - Дакле, неки љубимци имају крзно.

   ::

      fof(prem1, axiom, ![X]: (zec(X) => krzno(X))).
      fof(prem2, axiom, ?[X]: ljubimac(X) & zec(X)).
      fof(concl, conjecture, ?[X]: (zec(X) & krzno(X))).
     

   - Нема забавног домаћег.
   - Неке књиге се читају за домаћи.
   - Неке књиге нису забавне.

   ::

      fof(prem1, axiom, ~(?[X]: (domaci(X) & zabavan(X)))).
      fof(prem2, axiom, ?[X]: knjiga(X) & domaci(X)).
      fof(concl, conjecture, ?[X]: (knjiga(X) & ~zabavan(X))).
     
   - Сви људи су смртни.
   - Сви Грци су људи.
   - Неки Грци су смртни.
     
   ::

      fof(premisa1, axiom, ![X]: (grk(X) => covek(X))).
      fof(premisa2, axiom, ![X]: (covek(X) => smrtan(X))).
      fof(dodatna_premisa, axiom, ?[X]: grk(X)).
      fof(zakljucak, conjecture, ?[X]: (grk(X) & smrtan(X))).

   Приметимо да је у овом примеру било неопходно додати претпоставку
   да постоји бар један Грк. Наиме, Аристотел је подразумевао
   имплицитно да сваки предикат о коме се говори мора да буде
   задовољен за бар неки објекат (да су сви скупови објеката које
   разматрамо непразни), док у савременој логици та имплицитна
   претпоставка не постоји и потребно је експлицитно навести. Поставља
   се питање шта се дешава са тачношћу исказа „Сви Грци су смртни“ ако
   не постоји ни један Грк. Ово може бити донекле збуњујуће, али је у
   савременој логици недвосмислено да је исказ и тада тачан. Ипак, у
   неким ранијим разматрањима логике исказ је у тој ситуацији сматран
   тачним, у неким погрешим, а у неким неодређеним.
    
      
.. questionnote::

    Употребимо сада аутоматски доказивач теорема да докажемо да смо
    успешно решили један детективски случај.
     
    - Алиса, њен муж, син, ћерка и брат су ликови у роману Агате Кристи.
    - Једно од њих петоро је убило неког од преосталих четворо.
    - Мушка и женска особа су биле заједно у бару у време убиства.
    - Жртва и убица су били заједно на плажи у време убиства.
    - Једно дете је било само у време убиства.
    - Жртвин близанац/близнакиња није убица.
    - Убица је млађи од жртве.
     
    Кодирајмо сада ово знање помоћу предикатске логике (вама за вежбу
    остављамо да исто урадите помоћу исказне логике и случај решите помоћу
    SAT решавача).
     
    У случај је укључено пет особа. Уведимо предикат
    :math:`\mathrm{osoba}(x)` и пет константи :math:`\mathrm{alisa}`,
    :math:`\mathrm{muz}`, :math:`\mathrm{cerka}`, :math:`\mathrm{sin}` и
    :math:`\mathrm{brat}`. Једине особе које су релевантне за овај случај
    су ове, што можемо кодирати следећом формулом.
     
    .. math::
     
       (\forall x)(\mathrm{osoba}(x) \Rightarrow x=\mathrm{alisa} \vee x=\mathrm{muz} \vee x=\mathrm{cerka} \vee x=\mathrm{sin} \vee x=\mathrm{brat})
     
    Нагласимо да се не подразумева да различите константе означавају
    различите објекте и да је понекад потребно увести експлицитно
    претпоставке типа :math:`\mathrm{alisa} \neq \mathrm{muz}`. Ипак, у
    овом задатку то неће бити потребно.
     
    Уводимо још две константе: :math:`\mathrm{ubica}` и
    :math:`\mathrm{zrtva}`. Из текста задатка је познато је да су оне међу
    ових пет особа, као и да су у питању различите особе. То кодирамо
    следећом формулом.
     
    .. math::
       \mathrm{osoba}(\mathrm{ubica}) \wedge \mathrm{osoba}(\mathrm{zrtva}) \wedge \mathrm{ubica} \neq \mathrm{zrtva}
       
    Мушка и женска особа су биле заједно у бару у време убиства. Можемо
    увести константе :math:`\mathrm{u\_baru\_musko}` и
    :math:`\mathrm{u\_baru\_zensko}` и формулу која описује особине ових
    константи. Имплицитно је јасно ко су мушке, а ко су женске особе, али
    то је потребно експлицитно кодирати. Женске особе можемо експлицитно
    набројати, а мушке особе дефинисати као оне особе које нису женске.
     
    .. math::
       (\forall x)(\mathrm{zensko}(x) \Leftrightarrow x=\mathrm{alisa} \vee x=\mathrm{cerka})\\
       (\forall x)(\mathrm{musko}(x) \Leftrightarrow \mathrm{osoba}(x) \wedge \neg\mathrm{zensko}(x))
     
    Сада можемо описати особине особа у бару.
     
    .. math::
     
       \mathrm{musko}(\mathrm{u\_baru\_musko}) \wedge \mathrm{zensko}(\mathrm{u\_baru\_zensko})
     
    Жртва и убица су били на плажи у време убиства. Ово можемо
    кодирати или тако што уведемо нове две константе за особе које су
    биле на плажи или, можда мало једноставније, тако што уведемо
    предикат :math:`\mathrm{na\_plazi}(x)`.
     
    .. math::
     
       \mathrm{na\_plazi}(\mathrm{zrtva}) \wedge \mathrm{na\_plazi}(\mathrm{ubica})
     
    Из текста задатка се имплицитно подразумева да особе не могу
    истовремено бити и на плажи и у бару, али то је неопходно експлицитно
    кодирати.
     
    .. math::
     
       \neg \mathrm{na\_plazi}(\mathrm{u\_baru\_musko}) \wedge \neg \mathrm{na\_plazi}(\mathrm{u\_baru\_zensko})
     
    Једно дете је било само у време убиства. Имплицитно је јасно да су
    једино деца син и ћерка, као и да особе у бару ни особе на плажи нису
    саме. То морамо експлицитно да кодирамо (а уједно ћемо искористити
    прилику и да дефинишемо ко су родитељи). Уводимо предикате
    :math:`\mathrm{dete}(x)`, :math:`\mathrm{roditelj}(x)` и
    :math:`\mathrm{samo}(x)`.
     
    .. math::
     
       (\forall x)(\mathrm{dete}(x) \Leftrightarrow x = \mathrm{cerka} \vee x = \mathrm{sin})\\
       (\forall x)(\mathrm{roditelj}(x) \Leftrightarrow x = \mathrm{alisa} \vee x = \mathrm{muz})\\
       (\forall x)(\mathrm{samo}(x) \Leftrightarrow \neg \mathrm{na\_plazi}(x) \wedge x \neq \mathrm{u\_baru\_musko} \wedge x \neq \mathrm{u\_baru\_zensko})
     
    Сада можемо да искажемо да постоји дете које је било само.
     
    .. math::
     
       (\exists x)(\mathrm{dete}(x) \wedge \mathrm{samo}(x))
     
    Алиса није била заједно са мужем у време убиства. То значи да њих
    двоје нису могли бити заједно на плажи нити заједно у бару. Довољно је
    да дефинишемо да су две различите особе на плажи заједно, и да су две
    особе у бару заједно и да кажемо да Алиса и муж нису били
    заједно. Приметимо да овим кодирамо само потребан смер (јер предикат
    :math:`\mathrm{zajedno}` не дефинишемо коришћењем еквиваленције, него
    само импликације).
     
    .. math::
     
       \mathrm{zajedno}(\mathrm{u\_baru\_musko}, \mathrm{u\_baru\_zensko})\\
       (\forall x_1)(\forall x_2)(x_1 \neq x_2 \wedge \mathrm{na\_plazi}(x_1) \wedge \mathrm{na\_plazi}(x_2) \Rightarrow \mathrm{zajedno}(x_1, x_2))\\
       (\forall x_1)(\forall x_2)(\mathrm{zajedno}(x_1, x_2) \Rightarrow \mathrm{zajedno}(x_2, x_1))\\
       \neg \mathrm{zajedno}(\mathrm{alisa}, \mathrm{muz})
       
       
    Жртвин близанац није убица. Ово значи да жртва сигурно има близанца
    (или близнакињу). Постоје два могућа пара близанаца: син и ћерка и
    Алиса и њен брат. Један од њих сигурно јесте пар близанаца, а други не
    мора бити, међутим, безбедно је кодирати да су оба пара близанци (јер
    за пар у коме није жртва није битно да ли јесу или нису близанци, па
    не смета да се кодира да јесу).
     
    .. math::
     
       (\forall x_1)(\forall x_2)(\mathrm{blizanci}(x_1, x_2) \Leftrightarrow \\
          (x_1 = \mathrm{sin} \wedge x_2 = \mathrm{cerka}) \vee \\
          (x_1 = \mathrm{cerka} \wedge x_2 = \mathrm{sin}) \vee \\
          (x_1 = \mathrm{alisa} \wedge x_2 = \mathrm{brat}) \vee \\
          (x_1 = \mathrm{brat} \wedge x_2 = \mathrm{alisa}))
     
    Постоји жртвин близанац и он није убица.
     
    .. math::
     
       (\exists x)(\mathrm{blizanci}(\mathrm{zrtva}, x) \wedge x \neq \mathrm{ubica})
     
    На крају још кодирамо услов да је убица млађи од жртве. To једино
    значи да убица не може да буде родитељ, а жртва дете (јер остале
    односе година заправо не познајемо). Довољно је да кодирамо да
    родитељи не могу бити млађи од деце и да је убица млађи од жртве.
     
    .. math::
     
       (\forall x_1)(\forall x_2)(\mathrm{roditelj}(x_1) \wedge \mathrm{dete}(x_2) \Rightarrow \neg \mathrm{mladji}(x_1, x_2))\\
       \mathrm{mladji}(\mathrm{ubica}, \mathrm{zrtva})
     
    Претходне услове можемо записати у формату TPTP, предати их
    доказивачу *Vampire* и он ће практично моментално потврдити да су
    наши закључци исправни. Можемо редом пробати закључке облика
    :math:`\mathrm{ubica} = \mathrm{alisa}`, :math:`\mathrm{ubica} =
    \mathrm{muz}` итд. и само један од њих ће успети да буде доказан
    (једино решење је да је муж убио брата, да је Алиса била са сином
    у бару, а да је ћерка била сама код куће).
       
    ::
     
       % Alisa, njen muž, sin, ćerka i brat su likovi u romanu Agate Kristi.
       % Jedno od njih petoro je ubilo nekog od preostalih četvoro.
       fof(osoba_def, axiom, ![X] : (osoba(X) <=>
                      X = alisa | X = muz | X = sin | X = cerka | X = brat)).
       fof(ubica_zrtva, axiom, osoba(ubica) & osoba(zrtva) & ubica != zrtva).
        
       % Muska i zenska osoba su bile zajedno u baru u vreme ubistva.
       fof(zensko_def, axiom, ![X] : (zensko(X) <=> X = alisa | X = cerka)).
       fof(musko_def, axiom, ![X] : (musko(X) <=> osoba(X) & ~zensko(X))).
       fof(u_baru, axiom, musko(u_baru_musko) & zensko(u_baru_zensko)).
        
       % Zrtva i ubica su bili zajedno na plazi u vreme ubistva.
       fof(na_plazi, axiom, na_plazi(ubica) & na_plazi(zrtva)).
       fof(plaza_bar, axiom, ~na_plazi(u_baru_musko) & ~na_plazi(u_baru_zensko)).
        
       % Jedno dete je bilo samo u vreme ubistva.
       fof(dete_def, axiom, ![X] : (dete(X) <=> X = sin | X = cerka)).
       fof(roditelj_def, axiom, ![X] : (roditelj(X) <=> X = alisa | X = muz)).
       fof(samo_def, axiom, ![X] : (samo(X) <=>
                     ~na_plazi(X) & X != u_baru_musko & X != u_baru_zensko)).
       fof(samo_dete, axiom, ?[X] : (dete(X) & samo(X))).
        
       % Alisa i muz nisu bili zajedno u vreme ubistva.
       fof(zajedno_bar, axiom, zajedno(u_baru_musko, u_baru_zensko)).
       fof(zajedno_na_plazi, axiom, ![X1, X2] :
              (na_plazi(X1) & na_plazi(X2) & X1 != X2 => zajedno(X1, X2))).
       fof(zajedno_sym, axiom, ![X1, X2] :
              (zajedno(X1, X2) => zajedno(X2, X1))).
       fof(zajedno_alisa_muz, axiom, ~zajedno(alisa, muz)).
        
       % Zrtvin blizanac nije ubica.
       fof(blizanci_def, axiom, ![X1, X2] :
              (blizanci(X1, X2) <=> (X1 = sin & X2 = cerka) |
                                    (X1 = cerka & X2 = sin) |
                                    (X1 = alisa & X2 = brat) |
                                    (X1 = brat & X2 = alisa))).
       fof(zrtvin_blizanac, axiom, ?[X] : (blizanci(zrtva, X) & X != ubica)).
        
       % Ubica je mladji od zrtve.
       fof(mladji_def, axiom, ![X1, X2] :
            (roditelj(X1) & dete(X2) => ~mladji(X1, X2))).
       fof(mladji_ubica, axiom, mladji(ubica, zrtva)).
        
       % Resenje slucaja
       fof(solution, conjecture,
             ubica = muz &
             zrtva = brat &
             u_baru_zensko = alisa &
             u_baru_musko = sin &
             samo(cerka)).
