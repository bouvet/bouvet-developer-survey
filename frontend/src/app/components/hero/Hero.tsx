const Survey = () => {
  return (
    <section id="intro" className="mx-auto justify-center max-w-7xl lg:px-8 text-lg pt-40">
      {/* TODO: Get text dynamically from Survey API? */}
      <h2 className="text-3xl font-bold mb-5">Introduksjon</h2>
      <p className="pb-5">
        I Bouvet har vi mange leveranser og prosjekter. Selv om de fleste av
        disse benytter teknologier som er godt etablerte og ofte gjenbrukes, ser
        vi stadig nye løsninger som dukker opp i markedet. Dette kan være nye
        teknologier som forbedrer eksisterende systemer eller erstatter
        utdaterte løsninger. Uansett om det gjelder valg av teknologi for en ny
        leveranse eller oppdatering av en eldre løsning, er det en viktig
        milepæl å både gi gode råd og ta riktige valg for hva som skal brukes.
      </p>
      <p className="pb-5">
        Derfor ønsker vi å kartlegge hva som brukes i leveranser i dag, men også
        hvilke erfaringer utviklerne har med teknologiene de har brukt
        tidligere. Slik kan vi identifisere trender på teknologier som vokser
        eller blir mindre relevante, og dermed få en oversikt over hvilken
        retning utviklingen tar fremover.
      </p>
      <p>
        Denne rapporten presenterer svarene fra undersøkelsen, og viser trendene
        våre innen programmeringsspråk, rammeverk, KI og sikkerhetsverktøy
      </p>
      <h3 className="mt-10 font-bold text-left">Topp 10:</h3>
      <p>
        Presenterer de 10 mest brukte teknologiene i leveranser i dag. Dette gjelder både det de som har svart på undersøkelsen jobber med daglig, men også det de har brukt utenfor jobb.
      </p>
      <h3 className="mt-10 font-bold text-left">Ønsket og Beundret:</h3>
      <p className="pb-5">
        Denne visualiseringen viser avstanden mellom utviklers ønsker om å bruke en teknolig (ønsket) og andelen som faktisk har brukt den siste året og ønsker og fortsette og bruke den (beundret).
        Avstanden mellom ønsket og beundret kan gi en indikasjon på om en teknologi overfår eller skuffer i forhold til forventningene. For eksempel viser C#, et av våre mest brukte programmeringsspråk, en relativt kort avstand mellom ønsket og beundret (&gt;10 prosentpoent).
      </p>
      <p>
        Dette tyder på at C# opprettholder sin popularitet ved praktis bruk. På den andre siden viser Rust, et toppvalg blant utviklere som ønsker å prøve en ny teknologi, en betydelig avstand (&gt;60 prosentpoeng).
        Dette antyder at Rust skaper interesse og beundering etterhvert som utviklere får erfaring med det. "Beundret og Ønsket" git verdifull innsikt i hvilke teknologier som er på vei opp og ned i popularitet.
      </p>
      <h3 className="mt-10 font-bold text-left">Datagrunnlag:</h3>
      <p className="pb-5">
        Hvert segment tar utgangspunkt i hvor mange som har svart på det gitte segmentet, og utgjør altså 100%. Prosenten er ikke ut totalt antall deltakere som deltok i undersøkelsen.
      </p>
    </section>
  );
};

export default Survey;
