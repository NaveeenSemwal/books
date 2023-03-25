import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  @Input() member: Member | undefined;
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = environment.baseUrl;
  user: User | undefined;


  constructor(private accountService: AccountService) {

    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {

      if (user) {
        this.user = user;
      }
    })
  }

  ngOnInit(): void {
    this.initializeUploader();  
  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {

    // This will directly upload the photos and add the new photo to the current member photos
    this.uploader = new FileUploader({

      url: environment.baseUrl + "v1/users/add-photo",
      authToken: 'Bearer ' + this.user?.token,  // Its not using Angular HTTP Request, so intercepter won't get used. So explicitly need to send token
      isHTML5: true,
      allowedFileType: ['image'], // Can accept any type of image
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,  // This is the max size cloudanary allows

    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item,response,status,Headers) => {

      if (response) {

        const photo = JSON.parse(response);
        this.member?.photos.push(photo);  
      }
    }
  }

}
