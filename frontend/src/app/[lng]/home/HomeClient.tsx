"use client";

import { RtkQueryFetcherStatus } from "../../../../shared/rtk/src";
import HomeLayout from "./HomeLayout";

// Component
export default function HomeClient() {
  // !todo const homeQuery = useHomeQuery(ENUM);
  // !todo const currentUserQuery = useCurrentUserQuery();

  return (
    <RtkQueryFetcherStatus
      errorNotificationQueries={[]}
      queries={[]}
      queriesProgressText={["homeQuery"]}
    >
      <HomeLayout />
    </RtkQueryFetcherStatus>
  );
}
