<template>
    	<!-- WRAPPER -->
    <div>
    
        <div v-if="errors" class="alert alert-danger alert-dismissible" role="alert">
            <div class="alert-message">
                {{ [errors] }}
            </div>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">�</span>
            </button>
        </div>
        <div v-if="responseMessage" class="alert alert-primary alert-dismissible" role="alert"> <div class="alert-message"> {{ responseMessage }} </div> </div>
        <div class="card-body">

            <div class="row">
                <div class="col-12 col-xl-4">
                    <div class="form-group">
                        <label class="form-label">Service Number</label>
                        <vuejsAutocomplete source="/api/PersonAPI/getAllPersonsByServiceNoLimited/"
                                           input-class="form-control"
                                           @selected="setValuePersonID"
                                           v-model="postBody.PersonID">
                        </vuejsAutocomplete>

                    </div>
                </div>

            </div>

            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Service Number</th>
                        <th>Name</th>
                        <th>Employment Date</th>
                        <th>Current Rank</th>
                        

                    </tr>
                </thead>
                <tbody>
                    <tr v-for="personel in pList">
                        <td>{{ personel.svC_NO }}</td>
                        <td>{{ personel.lastName }}</td>
                        <td>{{ personel.dateemployed }}</td>
                        <td>{{ personel.title }}</td>
                        
                    </tr>
                </tbody>
             

            </table>   
            
        </div>
        <div v-show="wantshow">
        <div class="row">
             <div class="col-sm-4">
                <label>Rank/Rate/Title</label>
                <select class="form-control" v-model="postBody.title" name="ranl">
                    <option v-for="rnk in rankList" v-bind:value="rnk.id" v-bind:key="rnk.id"> {{ rnk.description }} </option>
                </select>
            </div>
             <div class="col-md-4">
                <div class="form-group">
                    <label class="form-label">Beneficiary</label>
                    <input class="form-control" name="description" v-model="postBody.beneficiary" />

                </div>
            </div>
            <div class="col-sm-4">
                <label>Bank Name</label>
                <select class="form-control" v-model="postBody.bank" name="bank" required>
                    <option v-for="bank in bankList" v-bind:value="bank.id" v-bind:key="bank.id"> {{ bank.bankname }} </option>
                </select>
            </div>
            <div class="col-sm-4">
                <label>Account Number</label>
                <input class="form-control" name="acctno" v-model="postBody.acctno" required />
            </div>


         </div>
         <br/>
         <div class="row" v-show="this.rankid>=1 && this.rankid<10" >
            <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control"  name="description" v-model="postBody.rank01" />
                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate01" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
         </div>
        <div class="row" v-show="this.rankid>=2 && this.rankid<10" >
            <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control"  name="description" v-model="postBody.rank02"  />
                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate02" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
</div>
 <div class="row" v-show="this.rankid>=3 && this.rankid<10" >
            <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control"  name="description" v-model="postBody.rank03"  />
                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate03" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
</div>
        <div class="row" v-if="this.rankid>=4 && this.rankid<10" >
            <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control"  name="description" v-model="postBody.rank04" />

                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate04" input-class="form-control" placeholder="Date">></vuejsDatepicker>
              </div>
</div>
        <div class="row" v-if="this.rankid>=5 && this.rankid<10">
            <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control" name="description" v-model="postBody.rank05"  />

                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate05" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
            </div>
        <div class="row" v-if="this.rankid>=6 && this.rankid<10">
            <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control" name="description" v-model="postBody.rank06" />

                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate06" input-class="form-control" placeholder="Date">></vuejsDatepicker>
              </div>
            </div>
        <div class="row" v-if="this.rankid>=7 && this.rankid<10" >
             <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control"  name="description" v-model="postBody.rank07" />

                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate07" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
            </div>
        <div class="row" v-if="this.rankid>=8 && this.rankid<10">
             
              <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control" name="description" v-model="postBody.rank08" />

                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate08" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
            </div>


 <div class="row" v-if="this.rankid>=9" >
            <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control" value="S/LT" name="description" v-model="postBody.rank09" />
                </div>
            </div>
             <div class="col-sm-4">
         
                  <vuejsDatepicker v-model="postBody.prmdate09" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
         </div>
        <div class="row" v-if="this.rankid>=10" >
            <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control"  name="description" v-model="postBody.rank10"  />
                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate10" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
</div>
        <div class="row" v-if="this.rankid>=11" >
            <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control" name="description" v-model="postBody.rank11" />

                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate11" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
</div>
        <div class="row" v-if="this.rankid>=12">
            <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control" value="CDR" name="description" v-model="postBody.rank12"  />

                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate12" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
            </div>
        <div class="row" v-if="this.rankid>=13">
            <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control" value="CAPT" name="description" v-model="postBody.rank13" />

                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate13" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
            </div>
        <div class="row" v-if="this.rankid>=14" >
             <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control" value="CDRE" name="description" v-model="postBody.rank14" />

                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate14" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
            </div>
        <div class="row" v-if=" this.rankid>=15">
             
              <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control" value="R/Adm" name="description" v-model="postBody.rank15" />

                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate15" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
            </div>

        <div class="row" v-if="this.rankid>=16" >
            <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control" value="Adm" name="description" v-model="postBody.rank16" />

                </div>
            </div>
             <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate16" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
            </div>
        <div class="row" v-if="this.rankid>=17">
            <div class="col-md-4">
                <div class="form-group">
                    <input class="form-control" value="V/Adm" name="description" v-model="postBody.rank17" />
                </div>
            </div>
            <div class="col-sm-4">
              
                  <vuejsDatepicker v-model="postBody.prmdate17" input-class="form-control" placeholder="Date"></vuejsDatepicker>
              </div>
            </div>

        <div class="row">
                    <div class="col-12 ">
                        <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group">
                        <button class="btn btn-submit btn-primary" v-on:click="checkForm" type="submit">{{submitorUpdate}}</button>
                        </div>
                    </div>
                </div>
  

        </div>
    </div>
							
	<!-- END WRAPPER -->
</template>

<script>
 import vuejsDatepicker from 'vuejs-datepicker';
    import vuejsAutocomplete from 'vuejs-auto-complete'
   
export default {
     components: {
           vuejsDatepicker,
         vuejsAutocomplete
        },
    props:['fundtypeid'],
    data() { 
        return {
        responseMessage:'',
            errors: null,
            searchData: '',
            submitorUpdate : 'Submit',
            canProcess: true,
            pList: null,
             wantshow:false,
            myList:null,
            rankList:null,
            bankList:null,
            rankid:0,
        canProcess : true,
        postBody: {
                Batch:'',
                acctno:'',
                beneficiary:'',
                bank:0,
                title:0,
                PersonID:0,
                prmdate01: '',
                prmdate02: '',
                prmdate03: '',
                prmdate04: '',
                prmdate05: '',
                prmdate06: '',
                prmdate07: '',
                prmdate08: '',
                prmdate09: '',
                prmdate10: '',
                prmdate11: '',
                prmdate12: '',
                prmdate13: '',
                prmdate14: '',
                prmdate15: '',
                prmdate16: '',
                prmdate17: '',
                rank01: 'OS',
                rank02: 'SM',
                rank03: 'AB',
                rank04: 'LS',
                rank05: 'PO',
                rank06: 'WO',
                rank07: 'MWO',
                rank08: 'NWO',
                rank09: 'S/LT',
                rank10: 'LT',
                rank11: 'LT/CDR',
                rank12: 'CDR',
                rank13: 'CAPT',
                rank14: 'CDRE',
                rank15: 'R/ADM',
                rank16: 'ADM',
                rank17: 'V/ADM',



        }
        }
        },
        mounted() {
            this.$store.state.objectToUpdate = null,
        axios
                .get('/api/Rank/getAllRanks')
                .then(response => (this.rankList = response.data));
                axios
                .get('/api/Bank/getAllBanks')
                .then(response => (this.bankList = response.data));
           
     //this.setValuePersonID();
     },
     
    watch:{
        '$store.state.objectToUpdate':function (newVal, oldval) { 

               
        }
    },
        methods: {   
      
        setValuePersonID: function(result) {
             axios
              .get(`/api/Navip/getPersonByID/${result.value}`)
              .then(response => {this.pList=response.data;
              this.rankid=response.data.rankid;
              this.postBody.prmdate01=response.data.pn.prmdate01;
              this.postBody.prmdate02=response.data.pn.prmdate02;
              this.postBody.prmdate03=response.data.pn.prmdate03;
              this.postBody.prmdate04=response.data.pn.prmdate04;
              this.postBody.prmdate05=response.data.pn.prmdate05;
              this.postBody.prmdate06=response.data.pn.prmdate06;
              this.postBody.prmdate07=response.data.pn.prmdate07;
              this.postBody.prmdate08=response.data.pn.prmdate08;
              this.postBody.prmdate09=response.data.pn.prmdate09;
              this.postBody.prmdate10=response.data.pn.prmdate10;
              this.postBody.prmdate11=response.data.pn.prmdate11;
              this.postBody.prmdate12=response.data.pn.prmdate12;
              this.postBody.prmdate13=response.data.pn.prmdate13;
              this.postBody.prmdate14=response.data.pn.prmdate14;
              this.postBody.prmdate15=response.data.pn.prmdate15;
              this.postBody.prmdate16=response.data.pn.prmdate16;
              this.postBody.prmdate17=response.data.pn.prmdate17;
              this.postBody.prmdate18=response.data.pn.prmdate18;

              this.postBody.bank=response.data.pn.bank;
              this.postBody.acctno=response.data.pn.acctno;
              this.postBody.beneficiary=response.data.pn.beneficiary;
              this.postBody.title=response.data.pn.title2;
              //alert(response.data.pn.title2);
              this.wantshow=true;
              
              })
             
            },
            checkForm: function (e) {
            if (this.postBody.PersonID) {
              e.preventDefault();
              this.canProcess = false;
              this.postPost();
          }
          else{

          this.errors = [];
            this.errors.push('svcno is Required');
          }
        },
             postPost() {

                if(this.submitorUpdate == 'Submit'){
                   // alert('i am here');
                    axios.post(`/api/Navip/createNavip`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            this.canProcess = true;
                            if (response.data.responseCode == '200') {

                                   
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
             }
         
        }
};
</script>