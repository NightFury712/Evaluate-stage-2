import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule} from '@angular/common/http'
import { AppComponent } from './app.component';
import { BasebuttonComponent } from './components/base/basebutton/basebutton.component';
import { BasetextboxComponent } from './components/base/basetextbox/basetextbox.component';
import { CustomerlistComponent } from './views/dictionary/customer/customerlist/customerlist.component';
import { BasecheckboxComponent } from './components/base/basecheckbox/basecheckbox.component';
import { HeaderComponent } from './components/layout/header/header.component';
import { NavbarComponent } from './components/layout/navbar/navbar.component';
import { PageComponent } from './components/layout/page/page.component';
import { PagedevelopingComponent } from './components/layout/pagedeveloping/pagedeveloping.component';
import { AppRoutes } from './routers/router';
import { BaserouterlinkComponent } from './components/base/baserouterlink/baserouterlink.component';
import { BasetableComponent } from './components/base/basetable/basetable.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BasepaginationComponent } from './components/base/basepagination/basepagination.component';
import { BaseimportpopupComponent } from './components/base/baseimportpopup/baseimportpopup.component';
import { BaseexportpopupComponent } from './components/base/baseexportpopup/baseexportpopup.component';
import { FormsModule } from '@angular/forms';
import { BasespinnerComponent } from './components/base/basespinner/basespinner.component';
import { BaseradiobtnComponent } from './components/base/baseradiobtn/baseradiobtn.component';
import { BasepopupinfoComponent } from './components/base/basepopupinfo/basepopupinfo.component';

@NgModule({
  declarations: [
    AppComponent,
    BasebuttonComponent,
    BasetextboxComponent,
    CustomerlistComponent,
    BasecheckboxComponent,
    HeaderComponent,
    NavbarComponent,
    PageComponent,
    PagedevelopingComponent,
    BaserouterlinkComponent,
    BasetableComponent,
    BasepaginationComponent,
    BaseimportpopupComponent,
    BaseexportpopupComponent,
    BasespinnerComponent,
    BaseradiobtnComponent,
    BasepopupinfoComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(AppRoutes),
    HttpClientModule,
    FontAwesomeModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
