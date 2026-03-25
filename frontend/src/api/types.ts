// src/api/types.ts
import type { paths } from "./schema";

// Type for the GET /api/journal-entries 200 response body
export type GetJournalEntriesResponse =
  paths["/api/JournalEntries"]["get"]["responses"]["200"]["content"]["application/json"];

export type CreateNewJournalEntryRequest =
  NonNullable<paths["/api/JournalEntries"]["post"]["requestBody"]>["content"]["application/json"];

// If that response is an array, each element is a journal entry DTO
export type JournalEntryDto = GetJournalEntriesResponse[number];
