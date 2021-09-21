import { Routes } from "@angular/router";
import { PageComponent } from "../components/layout/page/page.component";
import { PagedevelopingComponent } from "../components/layout/pagedeveloping/pagedeveloping.component";
import { RouterLinkInfo } from "../resources/MISAConst" 

export const AppRoutes: Routes = [
  {path: RouterLinkInfo.Dashboard.href.replace("/", ""), component: PagedevelopingComponent},
  {path: RouterLinkInfo.Cash.href.replace("/", ""), component: PagedevelopingComponent},
  {path: RouterLinkInfo.Bank.href.replace("/", ""), component: PagedevelopingComponent},
  {path: RouterLinkInfo.Pu.href.replace("/", ""), component: PagedevelopingComponent},
  {path: RouterLinkInfo.Customer.href.replace("/", ""), component: PageComponent},
  {path: RouterLinkInfo.Sale.href.replace("/", ""), component: PagedevelopingComponent},
  {path: RouterLinkInfo.Invoice.href.replace("/", ""), component: PagedevelopingComponent},
]