const Survey = () => {
  return (
    <section
      id="developer_profile"
      className="mx-auto relative flex justify-center flex-col max-w-7xl lg:px-8 pb-20 text-lg h-screen snap-center"
    >
      {/* TODO: Get text dynamically from Survey API? */}
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
    </section>
  );
};

export default Survey;
