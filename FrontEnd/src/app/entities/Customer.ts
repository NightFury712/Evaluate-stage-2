import { BaseEntity } from "./BaseEntity";

export interface Customer extends BaseEntity {
  CustomerId: string,
  CustomerCode: string,
  PhoneNumber: string,
  CompanyPhoneNumber: string,
  PersonalTaxCode: string,
  Country: string,
  Province: string,
  District: string,
  Ward: string,
}